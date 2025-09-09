using UnityEngine;
using HTC.UnityPlugin.Vive;
using Valve.VR.InteractionSystem;
using System.Collections;
using TMPro;

public class CPRSimulator : MonoBehaviour
{
    public HandRole hand = HandRole.RightHand;

    // 미니게임 켜진동안 바깥 상호작용은 잠시 꺼두기
    [SerializeField] private PlayerController playerController;

    [Header("UI Elements")]
    [SerializeField] private RectTransform circle;     // 움직일 원/사각
    [SerializeField] private RectTransform barCenter;  // 기준 막대
    [SerializeField] private RectTransform safeSquare; // 성공 영역
    [SerializeField] private TMP_Text countdownText;   // 카운트다운 표시
    [SerializeField] private TMP_Text timerText;       // 남은시간 표시

    [Header("Settings")]
    [SerializeField] private float bpm = 100f;
    [SerializeField] private float duration = 30f;
    [SerializeField] private float perfectRange = 20f; // (기존 범위, 필요시 유지)

    private float timer;
    private bool isActive = false;
    private int successCount = 0;
    private int failCount = 0;

    void OnEnable()
    {
        playerController.enabled = false;
        timer = 0f;
        successCount = 0;
        failCount = 0;
        isActive = false;

        // 카운트다운 시작
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

        // 게임 시작
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
            isActive = false;
            Debug.Log("⏰ CPR 세션 종료");
            return;
        }

        // 남은 시간 표시
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(remain);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        // BPM 기준 중앙 통과 속도
        float beatPerSecond = bpm / 60f;
        float pingpong = Mathf.PingPong(Time.time * beatPerSecond, 1f);

        // 이동 범위 (movingSquare 크기 보정)
        float halfBarWidth = barCenter.rect.width / 2f;
        float halfSquareWidth = circle.rect.width / 2f;
        float maxRange = halfBarWidth - halfSquareWidth;

        float xPos = Mathf.Lerp(-maxRange, maxRange, pingpong);
        circle.anchoredPosition = new Vector2(xPos, circle.anchoredPosition.y);

        // VR 트리거 입력 체크 (SafeSquare 안에 들어갔는지)
        if (ViveInput.GetPressDown(hand, ControllerButton.Trigger))
        {
            Vector2 movingPos = circle.anchoredPosition;
            Vector2 safePos = safeSquare.anchoredPosition;
            Vector2 safeSize = safeSquare.rect.size;

            if (movingPos.x >= safePos.x - safeSize.x / 2f &&
                movingPos.x <= safePos.x + safeSize.x / 2f &&
                movingPos.y >= safePos.y - safeSize.y / 2f &&
                movingPos.y <= safePos.y + safeSize.y / 2f)
            {
                successCount++;
                Debug.Log("✅ Perfect CPR (SafeSquare)! 성공: " + successCount);
            }
            else
            {
                failCount++;
                Debug.Log("❌ Missed timing! 실패: " + failCount);
            }
        }
    }
}
