using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartTimerManager : MonoBehaviour
{
    [SerializeField]
    private float StartWaitTime = 3f;       //게임 시작 대기 시간(3초로 임시 설정)
    private float CurrentTime;              //진행 시간
    private bool IsRunning = false;         //타이머 작동 여부
    [SerializeField]
    private Text StartTimerText;            //타이머 텍스트 UI
    [SerializeField]
    private Text StartMessage;              //게임 시작 텍스트 UI

    void Start()
    {
        //UI를 비활성화하고 타이머를 초기화하고 타이머 시작
        StartTimerText.gameObject.SetActive(false);
        StartMessage.gameObject.SetActive(false);
        ResetTimer();
        StartTimer();
    }

    void Update()
    {
        //타이머가 작동 중이 아니거나 현재 상태가 게임 대기 중이 아니면 반환
        if (!IsRunning || GameStateManager.Instance == null || GameStateManager.Instance.CurrentState != GameState.Wait) return;

        CurrentTime -= Time.deltaTime;  //진행 시간을 델타타임 만큼 계속 감소
        UpdateTimerUI();                //진행 시간에 따라 UI도 업데이트

        if (CurrentTime <= 0)
        {
            //진행 시간이 0보다 작거나 같아지면 타이머를 멈추고 게임 상태를 진행 중으로 바꿈
            StartMessage.gameObject.SetActive(true);
            CurrentTime = 0;
            StopTimer();
            GameStateManager.Instance.SetState(GameState.Play);
            StartCoroutine(HideGameStartTimer());
        }
    }

    private void ResetTimer()
    {
        CurrentTime = StartWaitTime;
    }

    public void StartTimer()
    {
        //타이머가 진행되도록 하고 타이머 UI를 활성화
        IsRunning = true;
        StartTimerText.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        //타이머 진행을 멈추고 타이머 UI를 비활성화
        IsRunning = false;
        ResetTimer();       //재호출 시 0으로 되어 있는 것을 방지
        StartTimerText.gameObject.SetActive(false);
    }

    private void UpdateTimerUI()
    {
        //초, 센티초까지 보이도록 UI 세팅
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        int centiseconds = Mathf.FloorToInt((CurrentTime % 1f) * 100);

        StartTimerText.text = string.Format("{0:00}:{1:00}", seconds, centiseconds);
    }

    //0.5초 뒤에 게임 시작 UI를 사라지도록 하기 위한 Coroutine
    private IEnumerator HideGameStartTimer()
    {
        yield return new WaitForSeconds(0.5f);
        StartMessage.gameObject.SetActive(false);
    }
}
