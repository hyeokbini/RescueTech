using System.Collections;
using UnityEngine;
using System.Collections.Generic; 
using System.Linq;            


public enum ActionType
{
    Cushion,
    Elevator,
    GasValve,
    Cover
}

public class ActionData
{
    public string Description { get; private set; }
    public int Score { get; private set; }
    public bool IsCompleted { get; set; }

    public ActionData(string description, int score)
    {
        Description = description;
        Score = score;
        IsCompleted = false;
    }
}


public class EarthquakeScoreManager : MonoBehaviour
{
    public static EarthquakeScoreManager Instance;

    private Dictionary<ActionType, ActionData> actionMap;

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
            // 씬 전환 시에도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
            InitActions();
        }
    }

    private void InitActions()
    {
        actionMap = new Dictionary<ActionType, ActionData>
        {
            { ActionType.Cushion, new ActionData("쿠션으로 머리를 보호하였습니다.", 1) },
            { ActionType.Elevator, new ActionData("엘리베이터 탑승을 시도하였습니다.", -1) },
            { ActionType.GasValve, new ActionData("가스 밸브를 확인하였습니다.", 1) },
            { ActionType.Cover, new ActionData("테이블 안으로 이동해 몸을 보호하였습니다.", 1) },
        };
    }

    public void CompleteAction(ActionType type)
    {
        if (actionMap.ContainsKey(type)){
            actionMap[type].IsCompleted = true;
            Debug.Log(type + "미션 완료");
        }
    }

    public List<string> GetCompletedDescriptions()
    {
        return actionMap.Values
            .Where(a => a.IsCompleted)
            .Select(a => a.Description)
            .ToList();
    }
}
