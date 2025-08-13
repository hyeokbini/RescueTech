using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireRealModeFlowManager : MonoBehaviour
{
    public static FireRealModeFlowManager Instance;
    [SerializeField]
    private FireGameStateManager theGameStateManager;   //게임 상태 매니저를 사용하기 위한 변수
    [SerializeField]
    private TextMeshProUGUI scoreText;      //점수 텍스트 UI
    private int totalScore;         //총 점수
    [SerializeField] 
    private Transform player;
    [SerializeField] 
    private Transform respawnPoint;
    [SerializeField]
    private Towel towel;
    [SerializeField]
    private FireFadeController fireFadeController;
    [SerializeField]
    private GameObject FinishUI;
    [SerializeField]
    private GameObject textObject;  
    private TextMeshProUGUI textComponent;

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
        }
    }

    void Start()
    {
        //UI 비활성화
        if(scoreText != null)
            scoreText.gameObject.SetActive(false);
        theGameStateManager.SetState(FireGameState.Wait);
    }

    public void StartStage()
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != FireGameState.Play) return;
        ResetScore();               //점수 초기화
        Debug.Log("스테이지 시작");
    }

    private void ResetScore()
    {
        totalScore = 0;
    }

    public void AddScore(int amount)
    {
        //현재 상태가 Play가 아니면 스테이지 시작 않고 반환
        if(theGameStateManager.currentState != FireGameState.Play) return;
        totalScore += amount;       //amount만큼 총 점수에 더함
    }

    public void EndStage()
    {
        theGameStateManager.SetState(FireGameState.End);
    }

    public void ClearStage()
    {
        StartCoroutine(ClearCoroutine());
    }

    private IEnumerator ClearCoroutine()
    {
        // 1. 페이드 아웃 (화면 검게)
        fireFadeController.FadeOut();
        // 페이드 완료될 때까지 대기
        while (fireFadeController.IsFading) yield return null;
        // 2. 플레이어 위치 이동
        if (player != null && respawnPoint != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                player.position = respawnPoint.position;
                controller.enabled = true;
                Debug.Log($"[리스폰 완료] Player 위치: {player.position}");
            }
            else
                Debug.LogError("카메라 또는 캐릭터 컨트롤러를 찾지 못함");
        }
        towel.PutOffFace();
        // 3. 결과 UI 표시
        if (FinishUI != null && scoreText != null){
            scoreText.text = "점수: " + totalScore;
            scoreText.gameObject.SetActive(true);
            FinishUI.SetActive(true);
            textComponent = textObject.GetComponent<TextMeshProUGUI>();
            // 스코어매니저 인스턴스에 결과 받아와서 출력
            string results = FireScoreManager.Instance.GetFormattedResults();
            textComponent.richText = true;
            textComponent.text = results;
        }
        else
            Debug.Log("UI 할당되지 않음");
        // 4. 페이드 인 (원래 화면)
        fireFadeController.FadeIn();
        while (fireFadeController.IsFading) yield return null;
    }
}
