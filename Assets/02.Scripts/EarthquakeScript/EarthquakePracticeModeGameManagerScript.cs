// EarthquakePracticeModeGameManagerScript.cs
using System.Collections;
using UnityEngine;
using System.Linq;

public class EarthquakePracticeModeGameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject clearUIPanel;
    [SerializeField] private bool autoStepAdvance = true;
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private float delayBeforeNextStep = 10f;

    private int stageStepCount;
    private int currentStepCount = 0;
    private Coroutine autoStepCoroutine;

    // 자동으로 넘어갈 단계 인덱스 (0-based)
    private int[] autoUIIndex = { 1, 2, 5, 7 };

    public int CurrentStepCount => currentStepCount;
    public bool IsAutoStepRunning => autoStepCoroutine != null;

    private void Start()
    {
        // 텍스트 리스트 개수에 맞춰 마지막 인덱스 세팅 (0부터 시작이므로 -1)
        stageStepCount = 10;
        TriggerStep();
    }

    public void TriggerStep()
    {
        // 최종 단계 도달하면 클리어 UI만 띄우고 종료
        if (currentStepCount == stageStepCount)
        {
            CheckStageClear();
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

    private void CheckStageClear()
    {
        if (clearUIPanel != null)
            clearUIPanel.SetActive(true);
        else
            Debug.Log("UI 할당되지 않음");
    }
}
