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
    private int currentStep;        //현재 스테이지 스텝
    [SerializeField]
    private Canvas feedbackCanvas;
    [SerializeField]
    private TextMeshProUGUI feedbackText;      //점수 텍스트 UI
    private int totalScore;         //총 점수

    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private TyphoonPlayerScript player;
    [SerializeField]
    private WindowListManagerScript window;

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
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if (theGameStateManager.currentState != TyphoonGameState.Play) return;

        currentStep++;                                    //현재 스테이지 스텝을 +1
        Debug.Log($"스테이지 Step {currentStep} 시작");     //로그로 확인
        if (currentStep == stageStep)                      //현재 스텝과 전체 스텝이 같은지 체크                      
        {
            theGameStateManager.SetState(TyphoonGameState.End);  //스텝이 같다면 게임 상태를 종료로 업데이트
            return;
        }
    }

    public void FinishStage()
    {
        player.EndState();
        if (feedbackCanvas != null)
        {
            feedbackCanvas.gameObject.SetActive(true);
            feedbackText.text = GetFeedbackText();
        }
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
        feedBack += "점수 : " + totalScore;
        return feedBack;
    }
}
