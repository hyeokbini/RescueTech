using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFCaseTwo : MonoBehaviour
{
    [SerializeField] private IFPracticeModeGameManagerScript practiceStageManager;
    [SerializeField] private GameObject fireCover; // 두어야하는 오브젝트
    [SerializeField] private Collider targetZone; // 두어야하는 위치

    // 미션 완료 멘트
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private float postTextDuration = 10f;


    [SerializeField] private FireCoverScript fireCoverScript;

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
            var coverCol = fireCover.GetComponent<Collider>();
            if (fireCoverScript == null &&  coverCol == null && targetZone == null) {
                Debug.Log("null 오류");
                yield break;
            }
            Debug.Log(fireCoverScript.isCarrying);
            if(!fireCoverScript.isCarrying && targetZone.bounds.Intersects(coverCol.bounds)){
                missionCompleted = true;
                Debug.Log("2구역 미션 완료 → 다음 단계로 진행");
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
               practiceStageManager.CurrentStepCount == 2;
    }
}
