using UnityEngine;
using UnityEngine.UI;

public enum TyphoonGameState
{
    Wait,   //게임 대기 중
    Play,   //게임 진행 중
    End     //게임 종료
}

public class TyphoonGameStateManager : MonoBehaviour
{
    public TyphoonGameState currentState = TyphoonGameState.Wait; //현재 상태를 체크하기 위한 변수
    [SerializeField]
    private TyphoonRealModeManagerScript theStageFlowManager;   //실전모드 스테이지 플로우 매니저 변수

    public void SetState(TyphoonGameState NewState)
    {
        currentState = NewState;    //현재 상태를 새로운 상태로 변경

        switch (currentState)       //현재 상태에 따라 로그 메세지 출력
        {
            case TyphoonGameState.Wait:
                Debug.Log("게임 대기 중");
                break;
            case TyphoonGameState.Play:
                theStageFlowManager.StartStage();   //스테이지 시작
                Debug.Log("게임 진행 중");
                break;
            case TyphoonGameState.End:
                theStageFlowManager.FinishStage();  //스테이지 종료
                Debug.Log("게임 종료");
                break;
        }
    }
}
