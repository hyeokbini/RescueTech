using UnityEngine;
using HTC.UnityPlugin.Vive;
using Valve.VR.InteractionSystem;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CPRSimulator : MonoBehaviour
{
    [SerializeField]
    private ElectricPracticeManagerScript practiceGameManager;
    [SerializeField]
    private ElectricRealManagerScript realGameManager;
    public HandRole hand = HandRole.RightHand;

    [SerializeField] private PlayerController playerController;

    [Header("UI Elements")]
    [SerializeField] private RectTransform circle;
    [SerializeField] private RectTransform barCenter;
    [SerializeField] private RectTransform safeSquare;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text timerText;

    [Header("Result Feedback")]
    [SerializeField] private GameObject goodImage;
    [SerializeField] private GameObject badImage;
    private GameObject lastActiveImage;
    private Coroutine hideCoroutine;

    [Header("Health Gauge (실전 모드 전용)")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float healthDrainPerSecond = 2f;
    [SerializeField] private float successHealAmount = 5f;
    [SerializeField] private float failDamageAmount = 7f;

    [Header("Settings")]
    [SerializeField] private float bpm = 100f;
    [SerializeField] private float duration = 30f;
    [SerializeField] private float perfectRange = 20f;

    [Header("Open/Close Animation (Tablet style)")]
    [SerializeField] private RectTransform rootPanel;               // 전체패널 (슬라이드용)
    [SerializeField] private RectTransform tabletScreenRect;       // 내부 화면 (X축으로 펼치는 대상)
    [SerializeField] private CanvasGroup tabletScreenCanvasGroup;  // 내부 화면 알파
    [SerializeField] private float slideDuration = 0.4f;
    [SerializeField] private Ease slideEase = Ease.OutBack;
    [SerializeField] private float screenRevealDuration = 0.4f;
    [SerializeField] private Ease screenEase = Ease.OutCubic;

    private bool isAnimating = false;

    private float timer;
    private bool isActive = false;
    private bool isRealMode = false; // 실전 모드 여부
    private float currentHealth;

    private int successCount = 0;
    private int failCount = 0;

    void OnEnable()
    {
        // 플레이어 제어 잠금
        if (playerController != null)
            playerController.enabled = false;

        timer = 0f;
        successCount = 0;
        failCount = 0;
        isActive = false;

        isRealMode = ModeManagerScript.Instance != null && ModeManagerScript.Instance.isRealMode;

        // 생명 게이지 준비(단, 실제 표시/활성화는 Open 애니메이션과 연결 가능)
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(isRealMode);

            // 시작 체력을 50%로 변경
            currentHealth = maxHealth * 0.5f;
            healthBar.value = currentHealth / maxHealth;
        }


        // 이미지 초기화
        if (goodImage != null) goodImage.SetActive(false);
        if (badImage != null) badImage.SetActive(false);
        lastActiveImage = null;
        hideCoroutine = null;

        // 애니메이션으로 열기 -> 애니메이션 끝난 뒤 Countdown 시작
        PlayOpenAnimation();
    }

    void OnDisable()
    {
        // 안전하게 플레이어 제어 복구
        if (playerController != null)
            playerController.enabled = true;
    }

    IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Start!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        isActive = true;
    }

    void Update()
    {
        if (!isActive) return;

        // (기존 Update 로직: 타이머, 위치 이동, 입력 체크 등)
        timer += Time.deltaTime;
        float remain = Mathf.Max(0, duration - timer);

        if (remain <= 0f)
        {
            if (isRealMode)
            {
                realGameManager.getCompletedActionList[3] = true;
            }
            EndSession();
            return;
        }

        // 시간 표시
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(remain);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        // BPM 기준 이동
        float beatPerSecond = bpm / 60f;
        float pingpong = Mathf.PingPong(Time.time * beatPerSecond, 1f);

        float halfBarWidth = barCenter.rect.width / 2f;
        float halfSquareWidth = circle.rect.width / 2f;
        float maxRange = halfBarWidth - halfSquareWidth;
        float xPos = Mathf.Lerp(-maxRange, maxRange, pingpong);
        circle.anchoredPosition = new Vector2(xPos, circle.anchoredPosition.y);

        // health drain
        if (isRealMode && healthBar != null)
        {
            currentHealth -= healthDrainPerSecond * Time.deltaTime;
            healthBar.value = Mathf.Clamp01(currentHealth / maxHealth);

            // 체력 0이면 종료
            if (currentHealth <= 0f)
            {
                if (isRealMode)
                {
                    realGameManager.getCompletedActionList[3] = false;
                }
                Debug.Log("💀 CPR 실패 - 체력 소진");
                EndSession();
                return;
            }
        }

        // 입력 체크
        if (ViveInput.GetPressDown(hand, ControllerButton.Trigger))
        {
            Vector2 movingPos = circle.anchoredPosition;
            Vector2 safePos = safeSquare.anchoredPosition;
            Vector2 safeSize = safeSquare.rect.size;

            bool inSafeZone = movingPos.x >= safePos.x - safeSize.x / 2f &&
                              movingPos.x <= safePos.x + safeSize.x / 2f &&
                              movingPos.y >= safePos.y - safeSize.y / 2f &&
                              movingPos.y <= safePos.y + safeSize.y / 2f;

            if (inSafeZone)
            {
                successCount++;
                Debug.Log("✅ Perfect CPR! 성공: " + successCount);

                if (isRealMode)
                    Heal(successHealAmount);

                ShowResultImage(goodImage);
            }
            else
            {
                failCount++;
                Debug.Log("❌ Missed timing! 실패: " + failCount);

                if (isRealMode)
                    Damage(failDamageAmount);

                ShowResultImage(badImage);
            }
        }
    }

    private void ShowResultImage(GameObject target)
    {
        if (target == null) return;

        // 같은 이미지가 이미 켜져 있는 경우: 타이머만 재시작
        if (lastActiveImage == target && target.activeSelf)
        {
            // 기존 코루틴 재시작: 멈추고 다시 시작
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HideAfterDelay(target, 0.5f));
            return;
        }

        // 다른 이미지가 켜져 있다면 (종류가 다를 때만) 기존꺼 끄기
        if (lastActiveImage != null && lastActiveImage != target)
        {
            lastActiveImage.SetActive(false);

            // 기존 자동숨김 코루틴이 있으면 정리
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }
        }

        // 대상 이미지 켜기
        target.SetActive(true);
        lastActiveImage = target;

        // 자동으로 0.5초 후에 끄기
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideAfterDelay(target, 0.5f));
    }

    private IEnumerator HideAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        // 타깃이 여전히 마지막으로 켜진 이미지면 끈다
        if (target == lastActiveImage)
        {
            target.SetActive(false);
            lastActiveImage = null;
            hideCoroutine = null;
        }
    }

    private void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        if (healthBar != null) healthBar.value = currentHealth / maxHealth;
    }

    private void Damage(float amount)
    {
        currentHealth = Mathf.Max(0f, currentHealth - amount);
        if (healthBar != null) healthBar.value = currentHealth / maxHealth;
    }

    private void EndSession()
    {
        isActive = false;
        Debug.Log("⏰ CPR 세션 종료");

        if (isRealMode)
        {
            int scorePerSuccess = 10;
            int scorePerFail = -5;
            int totalCprScore = successCount * scorePerSuccess + failCount * scorePerFail;

            realGameManager.AddScore(totalCprScore);
            realGameManager.SetEndState();
        }
        else
        {
            practiceGameManager.IncreaseStageStep();
        }

        // 닫기 애니메이션 재생하고 최종적으로 비활성화
        PlayCloseAnimation();
    }

    // ----------------------- 애니메이션 관련 함수 (NewInventoryUI 방식 재사용) -----------------------
    private void PlayOpenAnimation()
    {
        if (isAnimating) return;
        isAnimating = true;

        gameObject.SetActive(true);

        // rootPanel 시작 위치 (화면 아래)
        if (rootPanel != null)
        {
            Vector2 startPos = new Vector2(rootPanel.anchoredPosition.x, -Screen.height * 0.5f);
            rootPanel.anchoredPosition = startPos;
        }

        if (tabletScreenCanvasGroup != null)
            tabletScreenCanvasGroup.alpha = 0f;

        Sequence seq = DOTween.Sequence();
        seq.Append(rootPanel.DOAnchorPosY(0f, slideDuration).SetEase(slideEase));
        seq.AppendCallback(() =>
        {
            // 내부 화면 리빌(가운데에서 X축으로 펼치며 알파 증가)
            PlayInnerReveal();
        });
    }

    private void PlayInnerReveal()
    {
        if (tabletScreenRect == null || tabletScreenCanvasGroup == null)
        {
            isAnimating = false;
            // 애니메이션 없으면 바로 카운트다운 시작
            StartCoroutine(CountdownRoutine());
            return;
        }

        // 초기 스케일
        tabletScreenRect.localScale = new Vector3(0f, 1f, 1f);

        Sequence seq = DOTween.Sequence();
        seq.Append(tabletScreenRect.DOScaleX(1f, screenRevealDuration * 0.8f).SetEase(screenEase));
        seq.Join(tabletScreenCanvasGroup.DOFade(1f, screenRevealDuration).SetEase(Ease.OutQuad));
        seq.OnComplete(() =>
        {
            tabletScreenRect.localScale = Vector3.one;
            tabletScreenCanvasGroup.alpha = 1f;
            isAnimating = false;

            // 애니메이션이 끝난 뒤 카운트다운 시작
            StartCoroutine(CountdownRoutine());
        });
    }

    private void PlayCloseAnimation()
    {
        if (isAnimating) return;
        isAnimating = true;

        Sequence seq = DOTween.Sequence();

        // 내부 화면 먼저 가로로 접으면서 페이드아웃
        if (tabletScreenRect != null && tabletScreenCanvasGroup != null)
        {
            seq.Append(tabletScreenRect.DOScaleX(0f, screenRevealDuration * 0.8f).SetEase(screenEase));
            seq.Join(tabletScreenCanvasGroup.DOFade(0f, screenRevealDuration).SetEase(Ease.OutQuad));
        }

        // 그리고 전체 패널 아래로 슬라이드
        if (rootPanel != null)
        {
            seq.Append(rootPanel.DOAnchorPosY(-Screen.height, slideDuration).SetEase(slideEase));
        }

        seq.OnComplete(() =>
        {
            // 초기화
            if (tabletScreenRect != null) tabletScreenRect.localScale = Vector3.one;
            if (tabletScreenCanvasGroup != null) tabletScreenCanvasGroup.alpha = 1f;

            isAnimating = false;

            // 비활성화 및 플레이어 제어 복구
            gameObject.SetActive(false);
            if (playerController != null) playerController.enabled = true;
        });
    }
    // ----------------------------------------------------------------------------------------------
}
