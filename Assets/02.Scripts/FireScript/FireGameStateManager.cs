using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireGameState
{
    Wait,   //게임 대기 중
    Play,   //게임 진행 중
    End     //게임 종료
}

public class FireGameStateManager : MonoBehaviour
{
    public FireGameState currentState = FireGameState.Wait; //현재 상태를 체크하기 위한 변수
    [SerializeField]
    private FireTimerManager theTimerManager;           //타이머 매니저를 이용하여 진행 상태 체크
    [SerializeField]
    private FireRealModeFlowManager theFireRealModeManager;   //실전모드 스테이지 플로우 매니저 변수

    public void SetState(FireGameState NewState)
    {
        currentState = NewState;    //현재 상태를 새로운 상태로 변경

        switch (currentState)       //현재 상태에 따라 로그 메세지 출력
        {
            case FireGameState.Wait:
                Debug.Log("게임 대기 중");
                break;
            case FireGameState.Play:
                theTimerManager.StartTimer();       //타이머 시작
                theFireRealModeManager.StartStage();   //스테이지 시작
                Debug.Log("게임 진행 중");
                break;
            case FireGameState.End:
                theTimerManager.StopTimer();        //타이머 멈춤
                theFireRealModeManager.ClearStage();  //스테이지 종료
                Debug.Log("게임 종료");
                break;
        }
    }
}
