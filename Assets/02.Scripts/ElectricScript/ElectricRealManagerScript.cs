using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ElectricRealManagerScript : MonoBehaviour
{
    [SerializeField]
    private ElectricGameStateManager theGameStateManager;   //게임 상태 매니저를 사용하기 위한 변수
    [SerializeField]
    private ElectricTimerManager timerManager;
    [SerializeField]
    private ElectricPlayerScript player;
    [SerializeField]
    private int stageStep;          //전체 스테이지 스텝
    private int currentStep;        //현재 스테이지 스텝
    [SerializeField]
    private Canvas feedbackCanvas;
    [SerializeField]
    private TextMeshProUGUI feedbackText;      //점수 텍스트 UI
    [SerializeField]
    private GameObject gradeText;      //등급 텍스트 오브젝트
    private int totalScore;         //총 점수

    [SerializeField]
    private TextUIManagerScript textManager;

    private Coroutine endingCoroutine;

    // 0. 장갑을 꼈는지
    // 1. 스위치를 눌렀는지 -> 0 없이 1 하면 게임오버
    // 2. 119 요청을 주변사람에게 했는지
    // 3. CPR을 제대로 했는지 -> 3이 false면 게임오버
    public bool[] getCompletedActionList = new bool[4];


    void Start()
    {
        if (!ModeManagerScript.Instance.isRealMode)
        {
            gameObject.SetActive(false);
            return;
        }
        timerManager.StartTimer();
        textManager.ActivateUIWithText();
    }

    public void StartStage()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if (theGameStateManager.currentState != ElectricGameState.Play) return;

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
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if (theGameStateManager.currentState != ElectricGameState.Play) return;

        currentStep++;                                    //현재 스테이지 스텝을 +1
        Debug.Log($"스테이지 Step {currentStep} 시작");     //로그로 확인
        if (currentStep == stageStep)                      //현재 스텝과 전체 스텝이 같은지 체크                      
        {
            theGameStateManager.SetState(ElectricGameState.End);  //스텝이 같다면 게임 상태를 종료로 업데이트
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
        gradeText.GetComponent<TextMeshProUGUI>().text = GetGradeText(); // ✅ 등급 표시 추가
    }

    IEnumerator EndingCoroutine()
    {
        yield return StartCoroutine(GetComponent<ElectricEndingFadeInOutScript>().FadeCoroutine());
        endingCoroutine = null;
    }

    public void SetEndState()
    {
        theGameStateManager.SetState(ElectricGameState.End);
    }

    private void ResetScore()
    {
        totalScore = 0;
    }

    public void AddScore(int amount)
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if (theGameStateManager.currentState != ElectricGameState.Play) return;

        totalScore += amount;       //amount만큼 총 점수에 더함
    }

    private string GetFeedbackText()
    {
        string feedBack = "";
        if (!getCompletedActionList[0])
        {
            feedBack += "게임 오버\n\n";
            feedBack += "장갑을 끼지 않고 구조를 시도하면 추가 사고가 일어날 수 있습니다!\n\n";
            feedBack += "그립 버튼으로 메인 씬으로 돌아가기";
            return feedBack;
            // 게임오버
        }
        if (!getCompletedActionList[3])
        {
            feedBack += "게임 오버\n\n";
            feedBack += "CPR이 제대로 진행되지 않았습니다.\n\n";
        }
        if (!getCompletedActionList[1])
        {
            feedBack += "전원 스위치를 내리지 않은 채 구조를 시도했습니다.\n\n";
        }
        if (!getCompletedActionList[2])
        {
            feedBack += "119 구조 요청을 진행하지 않았습니다.\n\n";
        }
        if (feedBack == "")
        {
            feedBack += "성공적으로 감전사고 환자에 대한 응급처치를 마쳤습니다!\n\n";
        }
        feedBack += "그립 버튼으로 메인 씬으로 돌아가기";
        return feedBack;
    }

    private string GetGradeText()
    {
        // 게임 오버 조건이면 무조건 F
        if (!getCompletedActionList[0] || !getCompletedActionList[3])
            return "F";

        // 점수 기준 우선 체크 (800점 이상이면 SS)
        if (totalScore >= 800)
            return "SS";

        // 완료된 태스크 개수 계산
        int completedCount = 0;
        foreach (bool done in getCompletedActionList)
        {
            if (done) completedCount++;
        }

        // 개수별 등급 분기
        switch (completedCount)
        {
            case 4: return "S"; // 완벽 수행
            case 3: return "A";
            case 2: return "B";
            case 1: return "C";
            default: return "F"; // 아무 것도 안함
        }
    }


}
