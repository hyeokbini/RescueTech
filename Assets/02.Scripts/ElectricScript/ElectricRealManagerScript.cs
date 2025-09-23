using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private int totalScore;         //총 점수

    [SerializeField]
    private TextUIManagerScript textManager;

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
        if (feedbackCanvas != null)
        {
            feedbackCanvas.gameObject.SetActive(true);
            feedbackText.text = GetFeedbackText();
        }
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
            return feedBack; // 게임오버
        }
        if (!getCompletedActionList[3])
        {
            feedBack += "게임 오버\n\n";
            feedBack += "CPR이 제대로 진행되지 않았습니다.\n\n";
            return feedBack; // 게임오버
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
        feedBack += "점수 : " + totalScore + "\n\n";
        feedBack += "그립 버튼으로 메인 씬으로 돌아가기";
        return feedBack;
    }
}
