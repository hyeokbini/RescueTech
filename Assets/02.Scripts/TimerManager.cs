using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 10f;   //시간 제한(10초로 임시 설정)
    private float currentTime;      //진행 시간
    private bool isRunning = false; //타이머 작동 여부
    [SerializeField]
    private Text UI_timerText;      //타이머 텍스트 UI
    [SerializeField]
    private Text UI_startTimerText; //시작 타이머 텍스트 UI
    [SerializeField]
    private Text UI_startMessage;   //게임 시작 텍스트 UI
    private float startCurrentTime; //시작 타이머 진행 시간

    void Start()
    {
        //UI 컴포넌트 연결 체크
        if (UI_timerText == null)
        {
            Debug.LogError("TimerText is not assigned to " + gameObject.name);
            enabled = false;
            return;
        }
        ResetTimer();       //진행 시간을 초기화
        UpdateTimerUI();
        //게임 시작 타이머 UI를 꺼뒀다가 코루틴 적용
        UI_startTimerText.gameObject.SetActive(false);
        UI_startMessage.gameObject.SetActive(false);
        StartCoroutine(StartTimerCoroutine(3f));
    }

    void Update()
    {
        //타이머가 작동 중이 아니거나 현재 상태가 게임 진행 중이 아니라면 반환
        if (!isRunning || GameStateManager.instance == null || GameStateManager.instance.currentState != GameState.Play) return;

        currentTime -= Time.deltaTime;  //진행 시간을 델타타임 만큼 계속 감소
        UpdateTimerUI();                //진행 시간에 따라 UI도 업데이트

        if (currentTime <= 0)
        {
            //진행 시간이 0보다 작거나 같아지면 게임 상태로 종료로 바꿈
            currentTime = 0;
            GameStateManager.instance.EndGame();
        }
    }

    private void ResetTimer()
    {
        currentTime = timeLimit;
    }

    public void StartTimer()
    {
        //타이머가 진행되도록 함
        isRunning = true;
    }

    public void StopTimer()
    {
        //타이머 진행을 멈춤
        isRunning = false;
        ResetTimer();       //재호출 시 0으로 되어 있는 것을 방지
        UI_timerText.text = "00:00:00";
    }

    private void UpdateTimerUI()
    {
        //분, 초, 센티초까지 보이도록 UI 세팅
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int centiseconds = Mathf.FloorToInt((currentTime % 1f) * 100);

        UI_timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }

    private IEnumerator StartTimerCoroutine(float time)
    {
        UI_startTimerText.gameObject.SetActive(true);   //게임 시작 타이머 UI 활성화
        startCurrentTime = time;                        //진행시간을 time으로 초기화
        while (startCurrentTime > 0)                    //진행시간이 0이 될 때까지 반복
        {
            startCurrentTime -= Time.deltaTime;
            UpdateStartTimerUI();
            if(startCurrentTime <= 0)
                UI_startTimerText.text = "00:00";
            yield return null;
        }
        GameStateManager.instance.SetState(GameState.Play); //게임 진행 중으로 상태 업데이트
        StartCoroutine(HideGameStartTimer());               //게임 시작 UI를 0.5초 뒤에 사라지도록 코루틴 적용
    }

    private void UpdateStartTimerUI()
    {
        //초, 센티초까지 보이도록 UI 세팅
        int seconds = Mathf.FloorToInt(startCurrentTime % 60);
        int centiseconds = Mathf.FloorToInt((startCurrentTime % 1f) * 100);

        UI_startTimerText.text = string.Format("{0:00}:{1:00}", seconds, centiseconds);
    }

    //0.5초 뒤에 게임 시작 UI를 사라지도록 하기 위한 Coroutine
    private IEnumerator HideGameStartTimer()
    {
        UI_startTimerText.gameObject.SetActive(false);
        UI_startMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        UI_startMessage.gameObject.SetActive(false);
    }
}
