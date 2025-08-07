using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakePlayer : MonoBehaviour
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private EarthquakePracticeModeGameManagerScript earthquakeStageManager;
    [SerializeField] private Transform hmdTransform;
    [SerializeField] private Collider tableAreaCollider;
    [SerializeField] private CharacterController characterController;

    [SerializeField]
    private int interactIndex = 6;
    public int InteractIndex => interactIndex;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    
    private float standingHeight;

    private void Start()
    {
        standingHeight = hmdTransform.position.y;
    }

    // 구조 개선할 수 있을 것 같음 
    void Update()
    {
        // 실전 모드일 때는 매번 숙였는지 검사한다.
        if(ModeManagerScript.Instance.isRealMode){
            HideUnderTable();
        }
        // 연습 모드일 때는
        else {
            // 텍스트가 나오지 않는 상태고 해당 단계일 때만 숙였는지 검사한다.
            if (earthquakeStageManager.CurrentStepCount == interactIndex && !earthquakeStageManager.IsAutoStepRunning) {
                HideUnderTable();
            }
        }
        
    }

    public void HideUnderTable()
    {   
        // 숙였는지 확인한다.
        float currentHeight = hmdTransform.position.y;
        bool isCrouching = currentHeight < standingHeight * 0.85f;

        // 테이블 아래인지 확인한다.
        bool isUnderTable = tableAreaCollider.bounds.Contains(hmdTransform.position);
        
        if (isCrouching && isUnderTable)
        {
            Debug.Log("숙임");

            // 실전 모드일 때는 가점
            if(ModeManagerScript.Instance.isRealMode){
                EarthquakeScoreManager.Instance.CompleteAction(ActionType.Cover);
            }
            // 연습 모드일 때는 다음 단계로 trigger
            else {
                earthquakeStageManager.TriggerStep();
            }
            hasInteracted = true;
            return;
        }
    
    }
}