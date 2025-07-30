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

/*
| 키워드(Enum)| 문구(String)       | 점수(int) | 상태(bool) |
| cushion   | "쿠션으로 머리 보호"   |  +1     | false(기본) |
| elevator  | "엘레베이터 이용 시도" | -1       | false(기본) |
| gasvalve  | "가스 불 잠금"       | +1       | false(기본) |
*/
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
            InitActions();
        }
    }

    // 필요한 오브젝트 선언
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

    // 해당 미션을 수행하면 호출될 함수
    public void CompleteAction(ActionType type)
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
