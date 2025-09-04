using System.Collections.Generic;
using UnityEngine;

public class IFPlayer : MonoBehaviour
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private IFPracticeModeGameManagerScript practiceStageManager;
    [SerializeField] private Transform hmdTransform;
    [SerializeField] private List<Collider> caseTriggers;

    private bool hasInteracted = false;
    private int lastStep = -1;


    private void Update()
    {
        if (hmdTransform == null || practiceStageManager == null || caseTriggers == null) return;

        // 현재 스텝
        int currentStep = practiceStageManager.CurrentStepCount;

        // 첫 번째 스텝은 콜라이더 감지 안함
        if (currentStep <= 0) return; 

        // 인트로 제외 ( -1 ) 현재 스텝과 콜라이더 인덱스 일치 시키기
        int colliderIndex = currentStep - 1; 

        // 콜라이더 인덱스가 범위를 벗어나면 종료
        if (colliderIndex < 0 || colliderIndex >= caseTriggers.Count) return;

        if (currentStep != lastStep) {
            hasInteracted = false;   // 단계 바뀌면 리셋
            lastStep = currentStep;
        }

        // 현재 단계에 해당하는 콜라이더
        Collider currentStepCollider = caseTriggers[colliderIndex];
        if (currentStepCollider == null) return;

        // HMD가 올바른 구역의 콜라이더 안에 있을 때만 트리거
        if (!hasInteracted && currentStepCollider.bounds.Contains(hmdTransform.position))
        {
            Debug.Log($"플레이어가 접근한 콜라이더 인덱스: {colliderIndex}");
            practiceStageManager.TriggerStep(); // 해당 단계 안내 출력
            hasInteracted = true;
        }
    }
}
