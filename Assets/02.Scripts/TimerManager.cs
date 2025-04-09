using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float TimeLimit = 10f;   //시간 제한(10초로 임시 설정)
    private float CurrentTime;      //진행 시간
    private bool IsRunning = false; //타이머 작동 여부
    [SerializeField]
    private Text TimerText;         //타이머 텍스트 UI

    void Start()
    {
        ResetTimer();       //진행 시간을 초기화
        UpdateTimerUI();
    }

    void Update()
    {
        //타이머가 작동 중이 아니거나 현재 상태가 게임 진행 중이 아니라면 반환
        if (!IsRunning || GameStateManager.Instance.CurrentState != GameState.Play) return;

        CurrentTime -= Time.deltaTime;  //진행 시간을 델타타임 만큼 계속 감소
        UpdateTimerUI();                //진행 시간에 따라 UI도 업데이트

        if (CurrentTime <= 0)
        {
            //진행 시간이 0보다 작거나 같아지면 게임 상태로 종료로 바꿈
            CurrentTime = 0;
            GameStateManager.Instance.EndGame();
        }
    }

    private void ResetTimer()
    {
        CurrentTime = TimeLimit;
    }

    public void StartTimer()
    {
        //타이머가 진행되도록 함
        IsRunning = true;
    }

    public void StopTimer()
    {
        //타이머 진행을 멈춤
        IsRunning = false;
        TimerText.text = "00:00:00";
    }

    private void UpdateTimerUI()
    {
        //분, 초, 센티초까지 보이도록 UI 세팅
        int minutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        int centiseconds = Mathf.FloorToInt((CurrentTime % 1f) * 100);

        TimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
}
