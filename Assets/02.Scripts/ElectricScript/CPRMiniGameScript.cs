using UnityEngine;
using HTC.UnityPlugin.Vive;
using Valve.VR.InteractionSystem;
using System.Collections;
using TMPro;
using UnityEngine.UI; // ⬅️ 슬라이더용

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
    [SerializeField] private Slider healthBar;  // ⬅️ 슬라이더로 표현되는 세로 게이지
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float healthDrainPerSecond = 2f;
    [SerializeField] private float successHealAmount = 5f;
    [SerializeField] private float failDamageAmount = 7f;

    [Header("Settings")]
    [SerializeField] private float bpm = 100f;
    [SerializeField] private float duration = 30f;
    [SerializeField] private float perfectRange = 20f;

    private float timer;
    private bool isActive = false;
    private bool isRealMode = false; // 실전 모드 여부
    private float currentHealth;

    private int successCount = 0;
    private int failCount = 0;

    void OnEnable()
    {
        playerController.enabled = false;
        timer = 0f;
        successCount = 0;
        failCount = 0;
        isActive = false;

        // 실전 모드 여부 확인 (예시: 매니저에서 플래그 가져오기)
        isRealMode = ModeManagerScript.Instance.isRealMode;

        // ⬇️ 생명 게이지 초기화
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(isRealMode);
            currentHealth = maxHealth;
            healthBar.value = 1f;
        }

        // 이미지 초기화
        if (goodImage != null) goodImage.SetActive(false);
        if (badImage != null) badImage.SetActive(false);
        lastActiveImage = null;
        hideCoroutine = null;

        StartCoroutine(CountdownRoutine());
    }

    void OnDisable()
    {
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

        // 제한 시간 체크
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

        // 생명 게이지 감소 (실전 모드 전용)
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
        healthBar.value = currentHealth / maxHealth;
    }

    private void Damage(float amount)
    {
        currentHealth = Mathf.Max(0f, currentHealth - amount);
        healthBar.value = currentHealth / maxHealth;
    }

    private void EndSession()
    {
        isActive = false;
        Debug.Log("⏰ CPR 세션 종료");
        if (isRealMode)
        {
            // 성공 횟수에 따른 점수 추가
            int scorePerSuccess = 10;   // 성공 1회당 10점
            int scorePerFail = -5;      // 실패 1회당 -5점
            int totalCprScore = successCount * scorePerSuccess + failCount * scorePerFail;

            realGameManager.AddScore(totalCprScore);
            realGameManager.SetEndState();
        }
        else
        {
            practiceGameManager.IncreaseStageStep();
        }
        gameObject.SetActive(false);
    }
}
