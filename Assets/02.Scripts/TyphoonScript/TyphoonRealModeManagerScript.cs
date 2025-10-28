using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TyphoonRealModeManagerScript : MonoBehaviour
{
    [SerializeField]
    private TyphoonGameStateManager theGameStateManager;   //게임 상태 매니저를 사용하기 위한 변수
    [SerializeField]
    private int stageStep;          //전체 스테이지 스텝
    public int currentStep;        //현재 스테이지 스텝
    [SerializeField]
    private Canvas feedbackCanvas;
    [SerializeField]
    private TextMeshProUGUI feedbackText;      //점수 텍스트 UI
    [SerializeField]
    private GameObject gradeText;      //등급 텍스트 오브젝트
    private int totalScore;         //총 점수

    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private TyphoonPlayerScript player;
    [SerializeField]
    private WindowListManagerScript window;

    private Coroutine endingCoroutine;

    // 0. 테이프로 창문을 다 막았는지
    // 1. 누전 차단기를 내렸는지
    // 2. 연락 수단을 구비했는지
    public bool[] getCompletedActionList = new bool[3];


    void Start()
    {
        if (!ModeManagerScript.Instance.isRealMode)
        {
            gameObject.SetActive(false);
            return;
        }
        textManager.ActivateUIWithText();
    }

    public void StartStage()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if (theGameStateManager.currentState != TyphoonGameState.Play) return;

        ResetStep();                //스텝 초기화
        ResetScore();               //점수 초기화
        Debug.Log("스테이지 시작");
    }

    private void ResetStep()
    {
        currentStep = 0;
    }

    public void IncreaseStep()
    {
        Debug.Log($"IncreaseStep 호출됨, currentState = {theGameStateManager?.currentState}");
        if (theGameStateManager.currentState != TyphoonGameState.Play)
        {
            Debug.LogWarning("게임 상태가 Play가 아님 → return");
            return;
        }

        currentStep++;
        Debug.Log($"스테이지 Step {currentStep} 시작");
        if (currentStep == stageStep)
        {
            theGameStateManager.SetState(TyphoonGameState.End);
            return;
        }
    }

    public void FinishStage()
    {
        player.EndState();
        foreach (var c in player.GetComponentsInChildren<PlayerController>(true))
        {
            c.enabled = false;
        }
        textManager.DeactiveUIWithText();
        endingCoroutine = StartCoroutine(EndingCoroutine());
    }

    public void SetFeedBack()
    {
        feedbackCanvas.gameObject.SetActive(true);
        feedbackText.text = GetFeedbackText();
        gradeText.SetActive(true);
        gradeText.GetComponent<TextMeshProUGUI>().text = GetGradeText();
    }

    IEnumerator EndingCoroutine()
    {
        yield return StartCoroutine(GetComponent<TyphoonEndingFadeInOutScript>().FadeCoroutine());
        endingCoroutine = null;
    }

    private void ResetScore()
    {
        totalScore = 0;
    }

    public void AddScore(int amount)
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if (theGameStateManager.currentState != TyphoonGameState.Play) return;

        totalScore += amount;       //amount만큼 총 점수에 더함
    }

    private string GetFeedbackText()
    {
        string feedBack = "";
        if (!getCompletedActionList[0])
        {
            feedBack += "테이프로 막지 못한 창문이 있습니다. (" + window.currentWindowCount + "/" + window.allWindowCount + ")\n\n";
        }
        if (!getCompletedActionList[1])
        {
            feedBack += "누전 차단기를 내리지 않았습니다.\n\n";
        }
        if (!getCompletedActionList[2])
        {
            feedBack += "연락 수단을 구비하지 않았습니다.\n\n";
        }
        if (feedBack == "")
        {
            feedBack += "시간 내에 모든 대비를 마쳤습니다!\n\n";
        }
        feedBack += "점수 : " + totalScore + "\n\n";
        feedBack += "그립 버튼으로 메인 씬으로 돌아가기";
        return feedBack;
    }

    private string GetGradeText()
    {
        // 완료된 태스크 개수 계산
        int completedCount = 0;
        foreach (bool done in getCompletedActionList)
        {
            if (done) completedCount++;
        }

        // 완료 개수 기준 등급 분기
        switch (completedCount)
        {
            case 3: return "S"; // 모든 태스크 완료
            case 2: return "A";
            case 1: return "B";
            default: return "C"; // 0개 완료
        }
    }

}
