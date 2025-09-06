using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class CPRSimulator : MonoBehaviour
{
    public HandRole hand = HandRole.RightHand;

    [Header("UI Elements")]
    [SerializeField] private RectTransform circle;     // 움직일 원/사각
    [SerializeField] private RectTransform barCenter;  // 기준 막대
    [SerializeField] private RectTransform safeSquare; // 성공 영역

    [Header("Settings")]
    [SerializeField] private float bpm = 100f;        
    [SerializeField] private float duration = 30f;    
    [SerializeField] private float perfectRange = 20f; // (기존 범위, 필요시 유지)

    private float timer;
    private bool isActive = false;

    void OnEnable()
    {
        timer = 0f;
        isActive = true;
    }

    void Update()
    {
        if (!isActive) return;

        // 제한 시간 체크
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            isActive = false;
            Debug.Log("⏰ CPR 세션 종료");
            return;
        }

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
                Debug.Log("✅ Perfect CPR (SafeSquare)!");
            }
            else
            {
                Debug.Log("❌ Missed timing!");
            }
        }
    }
}
