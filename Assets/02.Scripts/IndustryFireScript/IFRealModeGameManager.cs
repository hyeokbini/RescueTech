using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class IFRealModeGameManager : MonoBehaviour
{
    [SerializeField]
    private IFGameStateManager theGameStateManager;   //게임 상태 매니저를 사용하기 위한 변수
    [SerializeField]
    private int stageStep;          //전체 스테이지 스텝
    private int currentStep;        //현재 스테이지 스텝
    [SerializeField]
    private Text UI_scoreText;      //점수 텍스트 UI
    
    [SerializeField]
    private GameObject WaitUI;      
    
    
    [SerializeField]
    private GameObject FinishUI;    
    [SerializeField]
    private GameObject textObject;  
    private TextMeshProUGUI textComponent;


    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    [SerializeField] private IFFadeController IFFadeController;
    [SerializeField] private float fadeSpeed = 0.05f;

    private int totalScore;

    void Start()
    {
        theGameStateManager.SetState(IFGameState.Wait);
        // 삼초 대기 후 게임 시작, 추후 시작 버튼 등으로 개선 가능
        StartCoroutine(ShowWaitUI(3f));
    }


    IEnumerator ShowWaitUI(float delay)
    {
        WaitUI.SetActive(true);
        yield return new WaitForSeconds(delay);
        theGameStateManager.SetState(IFGameState.Play);
        StartStage();
    }

    public void StartStage()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != IFGameState.Play) return;
        
        ResetStep();                //스텝 초기화
        ResetScore();               //점수 초기화
        
        WaitUI.SetActive(false);
    }

    private void ResetStep()
    {
        currentStep = 0;
    }

    public void IncreaseStep()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != IFGameState.Play) return;

        currentStep++;                                    //현재 스테이지 스텝을 +1
        Debug.Log($"스테이지 Step {currentStep} 시작");     //로그로 확인
        if(currentStep == stageStep)                      //현재 스텝과 전체 스텝이 같은지 체크                      
        {
            theGameStateManager.SetState(IFGameState.End);  //스텝이 같다면 게임 상태를 종료로 업데이트
            return;
        }
    }

    public void FinishStage()
    {
        StartCoroutine(ClearSequence());
    }

    private void ResetScore()
    {
        totalScore = 0;
    }

    public void AddScore(int amount)
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != IFGameState.Play) return;

        totalScore += amount;       //amount만큼 총 점수에 더함
    }

    private IEnumerator ClearSequence()
    {
        Debug.Log("실전 모드 종료");
        // 페이드 아웃 & 인
        IFFadeController.FadeOut(fadeSpeed);
        while (IFFadeController.IsFading) yield return null;

        // 리스폰
        if (player != null && respawnPoint != null)
            player.position = respawnPoint.position;

        IFFadeController.FadeIn(fadeSpeed);
        while (IFFadeController.IsFading) yield return null;

        // 결과 UI 표시
        if (FinishUI != null){
/*
            FinishUI.SetActive(true);
            textComponent = textObject.GetComponent<TextMeshProUGUI>();
            // 스코어매니저 인스턴스에 GetCompletedDescriptions() 함수로 결과 받아와서
            List<string> resultDescriptions = IFScoreManager.Instance.GetCompletedDescriptions();
            // 합친 후
            string resultText = string.Join("\n", resultDescriptions); 
            // 화면에 출력
            textComponent.text = resultText;
*/
        }
        else
            Debug.Log("UI 할당되지 않음");
    }
}
