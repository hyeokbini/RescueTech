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
    public static GameStateManager Instance;        //싱글톤 패턴 적용을 위한 변수
    public GameState CurrentState = GameState.Wait; //현재 상태를 체크하기 위한 변수
    [SerializeField]
    private TimerManager TheTimerManager;           //타이머 매니저를 이용하여 진행 상태 체크
    [SerializeField]
    private Text FinishText;                        //종료 텍스트 UI

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        //종료 텍스트를 꺼두고 현재 상태를 대기 중으로 설정
        FinishText.gameObject.SetActive(false);
        SetState(GameState.Wait);
    }

    public void SetState(GameState NewState)
    {
        CurrentState = NewState;    //현재 상태를 새로운 상태로 변경

        switch (CurrentState)       //현재 상태에 따라 로그 메세지 출력
        {
            case GameState.Wait:
                Debug.Log("게임 대기 중");
                break;
            case GameState.Play:
                TheTimerManager.StartTimer();
                Debug.Log("게임 진행 중");
                break;
            case GameState.End:
                TheTimerManager.StopTimer();
                FinishText.gameObject.SetActive(true);
                Debug.Log("게임 종료");
                break;
        }
    }

    public void EndGame()
    {
        //게임 종료로 상태 변경
        SetState(GameState.End);
    }
}
