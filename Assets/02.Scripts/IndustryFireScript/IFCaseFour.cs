using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFCaseFour : MonoBehaviour
{
    [SerializeField] private IFPracticeModeGameManagerScript practiceStageManager;
    [SerializeField] private List<GameObject> targets = new List<GameObject>(); // 없애야하는 타겟 오브젝트들

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
            for (int i = 0; i < targets.Count; i++)
            {
                if (cleanedIndex.Contains(i)) continue;
                var t = targets[i];
                if (t == null || !t.activeInHierarchy)
                {
                    cleanedIndex.Add(i);
                    Debug.Log($"{i} 제거됨 ({cleanedIndex.Count}/{targets.Count})");
                }
            }

            if (cleanedIndex.Count >= targets.Count)
            {
                missionCompleted = true;
                Debug.Log("1구역 미션 완료 다음 단계로 진행");
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
               practiceStageManager.CurrentStepCount == 4;
    }
}
