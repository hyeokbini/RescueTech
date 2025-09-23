using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFCaseFive : MonoBehaviour
{
    [SerializeField] private IFPracticeModeGameManagerScript practiceStageManager;
    [SerializeField] private GameObject target; // 없애야하는 타겟 오브젝트

    // 미션 완료 멘트
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private float postTextDuration = 10f;

    private List<int> cleanedIndex = new List<int>();
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

            // 오브젝트 비활성화 검사
            if (target == null || !target.activeInHierarchy)
            {
                missionCompleted = true;
                Debug.Log("5구역 미션 완료");
                // 바로 종료 코루틴 실행
                StartCoroutine(practiceStageManager.ClearSequenceDirect());
                yield break;
                yield break;    
            }

        }
    }

    // 맞는 단계인지 검사
    private bool isRightStep()
    {
        return practiceStageManager != null &&
               practiceStageManager.CurrentStepCount == 5;
    }
}
