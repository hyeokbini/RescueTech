using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Wait,   //게임 대기 중
    Play,   //게임 진행 중
    End     //게임 종료
}

public class GameStateManager : MonoBehaviour
{
    public GameState currentState = GameState.Wait; //현재 상태를 체크하기 위한 변수
    [SerializeField]
    private TimerManager theTimerManager;           //타이머 매니저를 이용하여 진행 상태 체크
    [SerializeField]
    private RealModeStageFlowManager theStageFlowManager;   //실전모드 스테이지 플로우 매니저 변수

    void Start()
    {
        //UI 컴포넌트 연결 체크
        if (theTimerManager == null) return;
        theTimerManager.FinishUIOff();
    }

    public void SetState(GameState NewState)
    {
        currentState = NewState;    //현재 상태를 새로운 상태로 변경

        switch (currentState)       //현재 상태에 따라 로그 메세지 출력
        {
            case GameState.Wait:
                Debug.Log("게임 대기 중");
                break;
            case GameState.Play:
                theTimerManager.StartTimer();       //타이머 시작
                theStageFlowManager.StartStage();   //스테이지 시작
                Debug.Log("게임 진행 중");
                break;
            case GameState.End:
                theTimerManager.StopTimer();        //타이머 멈춤
                theStageFlowManager.FinishStage();  //스테이지 종료
                theTimerManager.FinishUIOn();       //종료 UI 활성화화
                Debug.Log("게임 종료");
                break;
        }
    }
}
