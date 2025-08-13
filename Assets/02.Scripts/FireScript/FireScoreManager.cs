using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public enum FireAction
{
    EmergencyAlarm,
    Phone_119,
    Phone_Another,
    Towel,
    Elevator,
    Stair,
    OutDoor,
    Fire
}

public class FireActionData
{
    public string Description { get; private set; }
    public int Score { get; private set; }
    public bool IsCompleted { get; set; }
    public bool IsFatal { get; set; }
    public FireAction ActionType { get; private set; }

    public FireActionData(FireAction action, string description, int score)
    {
        ActionType = action;
        Description = description;
        Score = score;
        IsCompleted = false;
        IsFatal = false;
    }
}

public class FireScoreManager : MonoBehaviour
{
    public static FireScoreManager Instance;

    private Dictionary<FireAction, FireActionData> actionMap;

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
        actionMap = new Dictionary<FireAction, FireActionData>
        {
            { FireAction.EmergencyAlarm, new FireActionData(FireAction.EmergencyAlarm, "비상벨을 눌러 화재 상황을 알렸습니다.", 1) },
            { FireAction.Phone_119, new FireActionData(FireAction.Phone_119, "휴대폰으로 119에 신고하였습니다.", 1) },
            { FireAction.Phone_Another, new FireActionData(FireAction.Phone_Another, "휴대폰으로 다른 행동을 하였습니다.", -1) },
            { FireAction.Towel, new FireActionData(FireAction.Towel, "수건으로 입과 코를 막았습니다.", 1) },
            { FireAction.Stair, new FireActionData(FireAction.Stair, "계단을 이용해 대피하였습니다.", 1) },
            { FireAction.Elevator, new FireActionData(FireAction.Elevator, "엘리베이터를 이용하여 대피하였습니다.", -1) },
            { FireAction.OutDoor, new FireActionData(FireAction.OutDoor, "건물 밖으로 대피하였습니다.", 1) },
            { FireAction.Fire, new FireActionData(FireAction.Fire, "화재에 휘말렸습니다.", -5) }
        };
    }

    // 해당 미션을 수행하면 호출될 함수
    public void CompleteAction(FireAction type, bool isFatal = false)
    {
        if (!actionMap.ContainsKey(type)) return;
        var data = actionMap[type];
        data.IsCompleted = true;
        if (isFatal) data.IsFatal = true;
        Debug.Log($"Action Completed: {type} (Fatal: {isFatal})");
    }

    public string GetFormattedResults()
    {
        // 우선순위 정렬(치명적 실패-점수(오름차순))
        var completed = actionMap.Values.Where(a => a.IsCompleted).ToList();
        var ordered = completed
            .OrderByDescending(a => a.IsFatal)      // 치명적 우선
            .ThenBy(a => a.Score)                   // 음수(잘못) 먼저 나오게
            .ToList();

        StringBuilder sb = new StringBuilder();
        foreach (var a in ordered)
        {
            string line = FormatLine(a);
            sb.AppendLine(line);
        }

        return sb.ToString().TrimEnd();
    }

    // 각각의 액션을 컬러/심볼로 포맷 (TextMeshPro 리치텍스트 사용)
    private string FormatLine(FireActionData a)
    {
        // 색상 지정
        const string fatalColor = "#FF3333";
        const string wrongColor = "#FF5555";
        const string goodColor = "#0066FF";

        string color;
        string prefix;

        if (a.IsFatal)
        {
            color = fatalColor;
            prefix = "치명: ";
        }
        else if (a.Score < 0)
        {
            color = wrongColor;
            prefix = "잘못: ";
        }
        else // score > 0
        {
            color = goodColor;
            prefix = "정상: ";
        }

        // Description을 색으로 감싸기 (접두사 + 설명)
        string formatted = $"<color={color}>{prefix}{a.Description}</color>";
        return formatted;
    }
}
