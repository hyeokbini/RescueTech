using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public enum GasAction
{
    EmergencyAlarm,
    WalkieTalkie,
    Valve,
    MaskGlove,
    Window,
    Person,
    OxygenTank,
    Phone_119,
    Phone_Another
}

public class GasActionData
{
    public string Description { get; private set; }
    public int Score { get; private set; }
    public bool IsCompleted { get; set; }
    public bool IsFatal { get; set; }
    public GasAction ActionType { get; private set; }

    public GasActionData(GasAction action, string description, int score)
    {
        ActionType = action;
        Description = description;
        Score = score;
        IsCompleted = false;
    }
}

public class GasLeakScoreManager : MonoBehaviour
{
    public static GasLeakScoreManager Instance;
    [SerializeField]
    private GasInfected gasInfected;
    private Dictionary<GasAction, GasActionData> actionMap;
    private bool gasInfectedInvoke = false;

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
        actionMap = new Dictionary<GasAction, GasActionData>
        {
            { GasAction.EmergencyAlarm, new GasActionData(GasAction.EmergencyAlarm, "비상벨을 눌러 가스 누출 상황을 알렸습니다.", 1) },
            { GasAction.WalkieTalkie, new GasActionData(GasAction.WalkieTalkie, "무전기로 담당자에게 연락을 취했습니다.", 1) },
            { GasAction.Valve, new GasActionData(GasAction.Valve, "누수 밸브를 눌러 통제선을 작동하였습니다.", 1) },
            { GasAction.MaskGlove, new GasActionData(GasAction.MaskGlove, "마스크와 장갑을 착용하여 피부 노출을 막았습니다.", 1) },
            { GasAction.Window, new GasActionData(GasAction.Window, "창문을 열어 환기하였습니다.", 1) },
            { GasAction.Person, new GasActionData(GasAction.Person, "가스에 노출된 사람을 환기가 되는 곳으로 대피시켰습니다.", 1) },
            { GasAction.OxygenTank, new GasActionData(GasAction.OxygenTank, "호흡기로 노출자에게 산소를 공급하였습니다.", 1) },
            { GasAction.Phone_119, new GasActionData(GasAction.Phone_119, "휴대폰으로 119에 신고하여 노출자의 병원 이송을 도왔습니다.", 1) },
            { GasAction.Phone_Another, new GasActionData(GasAction.Phone_Another, "휴대폰을 필요 없는 행동을 하였습니다.", -1) }
        };
    }

    // 해당 미션을 수행하면 호출될 함수
    public void CompleteAction(GasAction type)
    {
        if (!actionMap.ContainsKey(type)) return;
        var data = actionMap[type];
        data.IsCompleted = true;
        if(!gasInfectedInvoke) CheckCondition();
        else CheckClear();
        Debug.Log($"Action Completed: {type}");
    }

    // 기초 행동을 다 취하면 감염자 발생
    private void CheckCondition()
    {
        GasAction[] type = new GasAction[] { GasAction.EmergencyAlarm, GasAction.WalkieTalkie, GasAction.Valve, GasAction.MaskGlove, GasAction.Window };
        for (int i = 0; i < 5; i++)
        {
            if (!actionMap[type[i]].IsCompleted) return;
        }
        gasInfected.Infected();
        gasInfectedInvoke = true;
    }

    // 모든 행동을 다했으면 실전 모드 종료
    private void CheckClear()
    {
        GasAction[] type = new GasAction[] { GasAction.EmergencyAlarm, GasAction.WalkieTalkie, GasAction.Valve, GasAction.MaskGlove, GasAction.Window, GasAction.Person, GasAction.OxygenTank, GasAction.Phone_119 };
        for (int i = 0; i < 8; i++)
        {
            if (!actionMap[type[i]].IsCompleted) return;
        }
        GasLeakRealModeFlowManager.Instance.EndStage();
    }

    // 매뉴얼에 맞는 행동 배열
    private static readonly GasAction[] GradePool = new GasAction[]
    {
        GasAction.EmergencyAlarm, GasAction.WalkieTalkie, GasAction.Valve, GasAction.MaskGlove,
        GasAction.Window, GasAction.Person, GasAction.OxygenTank, GasAction.Phone_119
    };

    // 올바른 행동을 취한 개수 반환 함수
    private int CountCompletedAction() => GradePool.Count(a => actionMap[a].IsCompleted);
    // 잘못된 행동을 취했는지 체크(등급 하락 요소)
    private bool IsInteractPhoneAnother() => actionMap.TryGetValue(GasAction.Phone_Another, out var d) && d.IsCompleted;

    // 등급 반환
    public char ComputeGrade()
    {
        int cnt = CountCompletedAction();
        char grade = (cnt >= 8) ? 'A' : (cnt >= 5 ? 'B' : 'C');
        if (IsInteractPhoneAnother())
        {
            if (grade == 'A') grade = 'B';
            else if (grade == 'B') grade = 'C';
        }
        return grade;
    }

    public string GetGrade()
    {
        var g = ComputeGrade();
        return $"{g}";
    }

    public string GetFormattedResults()
    {
        // 우선순위 정렬(점수(오름차순))
        var completed = actionMap.Values.Where(a => a.IsCompleted).ToList();
        var ordered = completed
            .OrderByDescending(a => a.Score)        // 음수(잘못) 먼저 나오게
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
    private string FormatLine(GasActionData a)
    {
        // 색상 지정
        const string feedbackColor = "#FFFFFF";
        string color = feedbackColor;
        string prefix = "";
        // Description을 색으로 감싸기 (접두사 + 설명)
        string formatted = $"<color={color}>{prefix}{a.Description}</color>";
        return formatted;
    }
}
