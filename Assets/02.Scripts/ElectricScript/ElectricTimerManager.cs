using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElectricTimerManager : MonoBehaviour
{
    public float timeLimit = 100f;   //시간 제한(10초로 임시 설정)
    private float currentTime;      //진행 시간
    private bool isRunning = false; //타이머 작동 여부
    [SerializeField]
    private TextMeshProUGUI UI_timerText;
    [SerializeField]
    private ElectricGameStateManager theGameStateManager;   //게임 상태 매니저를 사용하기 위한 변수

    void Awake()
    {
        //UI 컴포넌트 연결 체크
        if (UI_timerText == null) return;
        ResetTimer();       //진행 시간을 초기화
        UpdateTimerUI();
        StartTimer();
    }

    void Update()
    {
        //타이머가 작동 중이 아니거나 현재 상태가 게임 진행 중이 아니라면 반환
        if (!isRunning || theGameStateManager == null || theGameStateManager.currentState != ElectricGameState.Play) return;

        currentTime -= Time.deltaTime;  //진행 시간을 델타타임 만큼 계속 감소
        UpdateTimerUI();                //진행 시간에 따라 UI도 업데이트

        if (currentTime <= 0)
        {
            //진행 시간이 0보다 작거나 같아지면 게임 상태로 종료로 바꿈
            currentTime = 0;
            theGameStateManager.SetState(ElectricGameState.End);
        }
    }

    private void ResetTimer()
    {
        currentTime = timeLimit;
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
        Debug.Log("타이머 세팅 완");
    }

    public void StartTimer()
    {
        //타이머가 진행되도록 함
        isRunning = true;
        theGameStateManager.SetState(ElectricGameState.Play);
        Debug.Log("타이머 시작");
    }
}
