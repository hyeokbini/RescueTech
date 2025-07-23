using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EarthquakeRealModeGameManager : MonoBehaviour
{
    [SerializeField]
    private EarthquakeGameStateManager theGameStateManager;   //게임 상태 매니저를 사용하기 위한 변수
    [SerializeField]
    private int stageStep;          //전체 스테이지 스텝
    private int currentStep;        //현재 스테이지 스텝
    [SerializeField]
    private Text UI_scoreText;      //점수 텍스트 UI
    
    [SerializeField]
    private GameObject WaitUI;      
    
    [SerializeField]
    private GameObject PlayingButtonUI;    
    
    [SerializeField]
    private GameObject FinishUI;    
    [SerializeField]
    private GameObject textObject;  
    private TextMeshProUGUI textComponent;


    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    [SerializeField] private EarthquakeFadeController earthquakeFadeController;
    [SerializeField] private float fadeSpeed = 0.05f;

    private int totalScore;         //총 점수

    void Start()
    {
        theGameStateManager.SetState(EarthquakeGameState.Wait);
        // 삼초 대기 후 게임 시작, 추후 버튼으로 수정 가능
        StartCoroutine(ShowWaitUI(3f));
    }


    IEnumerator ShowWaitUI(float delay)
    {
        WaitUI.SetActive(true);
        yield return new WaitForSeconds(delay);
        theGameStateManager.SetState(EarthquakeGameState.Play);
        StartStage();
    }

    public void StartStage()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != EarthquakeGameState.Play) return;
        
        ResetStep();                //스텝 초기화
        ResetScore();               //점수 초기화
        
        WaitUI.SetActive(false);
        PlayingButtonUI.SetActive(true);
    }

    private void ResetStep()
    {
        currentStep = 0;
    }

    public void IncreaseStep()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != EarthquakeGameState.Play) return;

        currentStep++;                                    //현재 스테이지 스텝을 +1
        Debug.Log($"스테이지 Step {currentStep} 시작");     //로그로 확인
        if(currentStep == stageStep)                      //현재 스텝과 전체 스텝이 같은지 체크                      
        {
            theGameStateManager.SetState(EarthquakeGameState.End);  //스텝이 같다면 게임 상태를 종료로 업데이트
            return;
        }
    }

    public void FinishStage()
    {
        /*
        if(UI_scoreText != null)
        {
            UI_scoreText.text = "점수: " + totalScore;
            UI_scoreText.gameObject.SetActive(true);
        }*/
        StartCoroutine(ClearSequence());
    }

    private void ResetScore()
    {
        totalScore = 0;
    }

    public void AddScore(int amount)
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != EarthquakeGameState.Play) return;

        totalScore += amount;       //amount만큼 총 점수에 더함
    }

    private IEnumerator ClearSequence()
    {
        Debug.Log("실전 모드 종료");
        earthquakeFadeController.FadeOut(fadeSpeed);
        while (earthquakeFadeController.IsFading) yield return null;

        if (player != null && respawnPoint != null)
            player.position = respawnPoint.position;

        earthquakeFadeController.FadeIn(fadeSpeed);
        while (earthquakeFadeController.IsFading) yield return null;

        if (FinishUI != null){

            FinishUI.SetActive(true);
            textComponent = textObject.GetComponent<TextMeshProUGUI>();
            List<string> resultDescriptions = EarthquakeScoreManager.Instance.GetCompletedDescriptions();
            string resultText = string.Join("\n", resultDescriptions); 
            textComponent.text = resultText;

        }
        else
            Debug.Log("UI 할당되지 않음");
    }
}
