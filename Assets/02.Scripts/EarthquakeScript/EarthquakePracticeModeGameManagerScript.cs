// EarthquakePracticeModeGameManagerScript.cs
using System.Collections;
using UnityEngine;
using System.Linq;

public class EarthquakePracticeModeGameManagerScript : MonoBehaviour, IManagerObjCount
{
    [SerializeField] private GameObject clearUIPanel;
    [SerializeField] private bool autoStepAdvance = true;
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private float delayBeforeNextStep = 10f;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    [SerializeField] private EarthquakeFadeController earthquakeFadeController;
    [SerializeField] private float fadeSpeed = 0.05f;

    private int stageStepCount = 9;
    private int currentStepCount = 0;
    private Coroutine autoStepCoroutine;

    // 자동으로 넘어갈 단계 인덱스 (0-based)
    private int[] autoUIIndex = { 1, 2, 5, 7};

    public int CurrentStepCount => currentStepCount;
    public bool IsAutoStepRunning => autoStepCoroutine != null;

    public int ObjCount => currentStepCount;

    private void Start()
    {
        TriggerStep();
    }

    public void TriggerStep()
    {
        // 최종 단계 도달하면 클리어 UI만 띄우고 종료
        if (currentStepCount == stageStepCount)
        {
            StartCoroutine(ClearSequence());
            return;
        }

        textUIManager.ActivateUIWithText();

        if (autoStepAdvance)
            autoStepCoroutine = StartCoroutine(NextStepAfterDelay());
    }

    private IEnumerator NextStepAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeNextStep);
        textUIManager.IncreaseIndex();
        currentStepCount++;

        if (autoUIIndex.Contains(currentStepCount))
            TriggerStep();

        autoStepCoroutine = null;
    }

    private IEnumerator ClearSequence()
    {
        Debug.Log("연습 모드 종료");
        // 1. 페이드 아웃 (화면 검게)
        earthquakeFadeController.FadeOut(fadeSpeed);
        // 페이드 완료될 때까지 대기
        while (earthquakeFadeController.IsFading) yield return null;

        // 2. 플레이어 위치 이동
        if (player != null && respawnPoint != null)
            player.position = respawnPoint.position;

        // 3. 페이드 인 (원래 화면)
        earthquakeFadeController.FadeIn(fadeSpeed);
        while (earthquakeFadeController.IsFading) yield return null;

        // 4. 클리어 UI 패널 띄우기
        if (clearUIPanel != null)
            clearUIPanel.SetActive(true);
        else
            Debug.Log("UI 할당되지 않음");
    }
}
