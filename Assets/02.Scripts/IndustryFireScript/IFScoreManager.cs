using System.Collections;
using UnityEngine;
using System.Collections.Generic; 
using System.Linq;            


public enum IFActionType
{
    RemovableItems,
    FireCover,
    CallSupervisor,
    StopPainting
}


public class IFActionData
{
    public string Description { get; private set; }
    public int Score { get; private set; }
    public bool IsCompleted { get; set; }

    public IFActionData(string description, int score)
    {
        Description = description;
        Score = score;
        IsCompleted = false;
    }
}


public class IFScoreManager : MonoBehaviour
{
    public static IFScoreManager Instance;

    private Dictionary<IFActionType, IFActionData> actionMap;

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            InitActions();
        }
    }

    // 필요한 오브젝트 선언
    private void InitActions()
    {
        actionMap = new Dictionary<IFActionType, IFActionData>
        {
            { IFActionType.RemovableItems, new IFActionData("가연성 물질을 제거했습니다.", 1) },
            { IFActionType.FireCover, new IFActionData("불티 방지 커버를 설치했습니다.", 1) },
            { IFActionType.CallSupervisor, new IFActionData("감독관을 호출했습니다.", 1) },
            { IFActionType.StopPainting, new IFActionData("도장 작업을 중지 요청했습니다.", 1) },
        };
    }

    // 해당 미션을 수행하면 호출될 함수
    public void CompleteAction(IFActionType type)
    {
        if (actionMap.ContainsKey(type)){
            actionMap[type].IsCompleted = true;
            Debug.Log(type + "미션 완료");
        }
    }

    // IsCompleted가 true인 value 중 Description을 선택해서 list로 반환
    public List<string> GetCompletedDescriptions()
    {
        return actionMap.Values
            .Where(a => a.IsCompleted)
            .Select(a => a.Description)
            .ToList();
    }
}
