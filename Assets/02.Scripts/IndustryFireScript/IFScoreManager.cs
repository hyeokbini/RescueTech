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
    public int CurrentCount { get; set; }
    public int TargetCount { get; set; } 

    public IFActionData(string description, int score, int targetCount = 1)
    {
        Description = description;
        Score = score;
        IsCompleted = false;
        CurrentCount = 0;
        TargetCount = targetCount;
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
            { IFActionType.RemovableItems, new IFActionData("가연성 물질을 제거했습니다.", 1, 8) },
            { IFActionType.FireCover, new IFActionData("불티 방지 커버를 설치했습니다.", 1) },
            { IFActionType.CallSupervisor, new IFActionData("감독관을 호출했습니다.", 1) },
            { IFActionType.StopPainting, new IFActionData("도장 작업을 중지 요청했습니다.", 1) },
        };
    }

    // 해당 미션을 수행하면 호출될 함수
    public void CompleteAction(IFActionType type)
    {
        if (actionMap.ContainsKey(type)){
            var action = actionMap[type];

            if (type == IFActionType.RemovableItems)
            {
                // '가연성 물질 제거' 미션인 경우, 개수 증가
                if (action.CurrentCount < action.TargetCount)
                {
                    action.CurrentCount++;
                    Debug.Log("가연성 물질 미션 진행: (" + action.CurrentCount + "/" + action.TargetCount + ")");
                }

                // 목표 개수에 도달하면 IsCompleted를 true로 설정
                if (action.CurrentCount == action.TargetCount)
                {
                    action.IsCompleted = true;
                    Debug.Log("가연성 물질 미션 완료");
                }
            }
            else
            {
                // 다른 미션은 즉시 완료 처리
                action.IsCompleted = true;
                Debug.Log(type + " 미션 완료");
            }
        }
    }

    // IsCompleted가 true인 value 중 Description을 선택해서 list로 반환
    public List<string> GetCompletedDescriptions()
    {
        return actionMap.Values
            // 미션을 완료한 경우 & 가연성 물질 제거 미션 1개 이상 성공한 경우 
            .Where(a => a.IsCompleted || (a.CurrentCount > 0 && actionMap.First(kv => kv.Value == a).Key == IFActionType.RemovableItems))
            .Select(a => {
                if (actionMap.ContainsValue(a) && actionMap.First(kv => kv.Value == a).Key == IFActionType.RemovableItems)
                {   
                    // 가연성 물질 제거 미션은 개수까지 표현
                    return $"{a.Description} ({a.CurrentCount}/{a.TargetCount})";
                }
                return a.Description;
            })
            .ToList();
    }

    public string GetGrade()
    {
        // 총 점수 계산
        int completedMission = actionMap.Values.Where(a => a.IsCompleted).Count();
        var scores = actionMap.Values
        .Select(a =>
        {
            // RemovableItems인 경우 CurrentCount를 반환
            if (actionMap.First(kv => kv.Value == a).Key == IFActionType.RemovableItems)
            {
                return a.CurrentCount;
            }
            // 그 외의 경우 IsCompleted가 true면 1, 아니면 0을 반환
            else
            {
                return a.IsCompleted ? 1 : 0;
            }
        })
        .Sum(); // 계산된 점수들의 합계
        

        // 총 점수가 11점이면 S 
        // 가연성 물질도 모두 치우고, 나머지 미션도 성공한 경우
        if (scores == 11){
            return "A";
        }
        // 총 4개 미션 중 n개만 미션을 성공했을 때
        switch (completedMission)
        {
            case 3:
                return "A";
            case 2:
                return "B";
            default:
                return "C";
        }


    }
}
