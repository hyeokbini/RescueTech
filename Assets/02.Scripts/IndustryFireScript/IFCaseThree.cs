using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFCaseThree : MonoBehaviour
{
    [SerializeField] private IFPracticeModeGameManagerScript practiceStageManager;
    [SerializeField] private int zoneStepIndex = 3;
    [SerializeField] private GameObject target; // 작업자 오브젝트

    // 미션 완료 멘트
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private float postTextDuration = 10f;

    private bool missionCompleted;
    private Coroutine checkMissionCoroutine;

    private void OnEnable()
    {
        if (checkMissionCoroutine == null) checkMissionCoroutine = StartCoroutine(checkMission());
    }

    private void OnDisable()
    {
        if (checkMissionCoroutine != null)
        {
            StopCoroutine(checkMissionCoroutine);
            checkMissionCoroutine = null;
        }
    }

    private IEnumerator checkMission()
    {
        var wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            if (!isRightStep() || missionCompleted) continue;

            if (target.activeInHierarchy)
            {
                missionCompleted = true;
                Debug.Log("3구역 미션 완료 다음 단계로 진행");
                StartCoroutine(clearedMission());
                yield break;
            }

        }
    }

    // 미션 완료 시 호출
    private IEnumerator clearedMission()
    {
        if (textUIManager != null)
        {
            textUIManager.IncreaseIndex();     
            textUIManager.ActivateUIWithText(); 
            yield return new WaitForSeconds(postTextDuration);
        }

        practiceStageManager.AdvanceStep();
    }

    // 맞는 단계인지 검사
    private bool isRightStep()
    {
        return practiceStageManager != null &&
               practiceStageManager.CurrentStepCount == 3;
    }
}
