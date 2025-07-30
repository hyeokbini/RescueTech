using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePracticeModeManager : MonoBehaviour, IManagerObjCount
{
    [SerializeField]
    private int stageStepCount; // 스테이지에서 진행해야 할 총 단계
    private int currentStepCount = 0; // 현재 단계
    [SerializeField]
    private GameObject clearUIPanel; // ui 오브젝트 연결
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private FireFadeController fireFadeController;
    [SerializeField] 
    private Transform player;
    [SerializeField] 
    private Transform respawnPoint;
    public int ObjCount => currentStepCount;
    [SerializeField]
    private Towel towel;

    void Start()
    {
        textManager.ActivateUIWithText();
    }

    // 각 오브젝트에 gamemanager를 참조해서 이 함수 실행
    public void IncreaseStageStep()
    {
        currentStepCount++;
        // 스테이지 클리어 로직 체크
        if(currentStepCount == stageStepCount)
        {
            CheckStageClear();
        }
    }

    // 스테이지 클리어 로직
    private void CheckStageClear()
    {
        StartCoroutine(ClearFade());
        if (clearUIPanel != null)
        {
            clearUIPanel.SetActive(true);
        }
        else
        {
            Debug.Log("UI 할당되지 않음");
        }
    }

    private IEnumerator ClearFade()
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
            {
                Debug.LogError("카메라 또는 캐릭터 컨트롤러를 찾지 못함");
            }
        }
        towel.PutOffFace();
        // 3. 페이드 인 (원래 화면)
        fireFadeController.FadeIn();
        while (fireFadeController.IsFading) yield return null;
    }
}
