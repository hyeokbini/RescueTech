using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealModeStageFlowManager : MonoBehaviour
{
    [SerializeField]
    private int stageStep;          //전체 스테이지 스텝
    private int currentStep;        //현재 스테이지 스텝
    [SerializeField]
    private Text UI_scoreText;      //점수 텍스트 UI
    private int totalScore;         //총 점수

    void Start()
    {
        if(UI_scoreText != null)
            UI_scoreText.gameObject.SetActive(false);       //UI 비활성화
    }

    public void StartStage()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(GameStateManager.instance.currentState != GameState.Play) return;
        
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
        if(GameStateManager.instance.currentState != GameState.Play) return;

        currentStep++;                                    //현재 스테이지 스텝을 +1
        Debug.Log($"스테이지 Step {currentStep} 시작");     //로그로 확인
        if(currentStep == stageStep)                      //현재 스텝과 전체 스텝이 같은지 체크                      
        {
            GameStateManager.instance.EndGame();          //스텝이 같다면 게임 상태 매니저의 EndGame 호출
            return;
        }
    }

    public void FinishStage()
    {
        if(UI_scoreText != null)
        {
            UI_scoreText.text = "점수: " + totalScore;
            UI_scoreText.gameObject.SetActive(true);
        }
    }

    private void ResetScore()
    {
        totalScore = 0;
    }

    public void AddScore(int amount)
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(GameStateManager.instance.currentState != GameState.Play) return;

        totalScore += amount;       //amount만큼 총 점수에 더함
    }
}
