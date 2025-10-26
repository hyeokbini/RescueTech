using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFPracticeModeGameManagerScript : MonoBehaviour, IManagerObjCount
{
    [SerializeField] private GameObject clearUIPanel;
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private IFFadeController fadeController;
    [SerializeField] private float fadeSpeed = 0.1f;

    [SerializeField] private float delayBeforeNextStep = 10f;

    // 자동으로 넘어갈 단계 인덱스 (0-based)
    private int[] autoUIIndex = { 0 };

    private int stageStepCount = 6;
    private int currentStepCount = 0;   
    private Coroutine autoStepCoroutine;

    public int CurrentStepCount => currentStepCount;
    public int ObjCount => currentStepCount;

    private void Start()
    {
        Debug.Log("연습모드 시작");
        TriggerStep(); 
    }

    // UI를 관리(다음 텍스트로 이동)
    public void TriggerStep()
    {
        // 종료 시퀀스
        if (currentStepCount >= stageStepCount)
        {
            StartCoroutine(ClearSequence());
            return;
        }

        if (textUIManager != null)
            textUIManager.ActivateUIWithText();
        
        //첫 번째 문구는 자동 다음 단계 진입
        if (currentStepCount == 0)
        {
            if (autoStepCoroutine != null)
            {
                StopCoroutine(autoStepCoroutine);
                autoStepCoroutine = null;
            }
            autoStepCoroutine = StartCoroutine(NextStepAfterDelay(currentStepCount));
        }
    }

    // 인트로 후 자동 진입 코루틴
    private IEnumerator NextStepAfterDelay(int scheduledStep)
    {
        yield return new WaitForSeconds(delayBeforeNextStep);

        // 그 사이 단계 변경되었으면 취소
        if (currentStepCount != scheduledStep)
        {
            autoStepCoroutine = null;
            yield break;
        }

        AdvanceStep();
        autoStepCoroutine = null;
    }

    // 다음 단계로 이동
    public void AdvanceStep()
    {
        if (autoStepCoroutine != null)
        {
            StopCoroutine(autoStepCoroutine);
            autoStepCoroutine = null;
        }

        if (textUIManager != null)
            textUIManager.IncreaseIndex();

        currentStepCount++;

        // 종료 시퀀스
        if (currentStepCount >= stageStepCount)
        {
            StartCoroutine(ClearSequence());
            return;
        }

    }
    public IEnumerator ClearSequenceDirect()
    {
        return ClearSequence();
    }

    private IEnumerator ClearSequence()
    {
        Debug.Log("연습 모드 종료");

        if (fadeController != null)
        {
            fadeController.FadeOut(fadeSpeed);
            while (fadeController.IsFading) yield return null;

            fadeController.FadeIn(fadeSpeed);
            while (fadeController.IsFading) yield return null;
        }

        if (clearUIPanel != null) clearUIPanel.SetActive(true);
        else Debug.Log("클리어 UI 패널 미할당");
    }
}
