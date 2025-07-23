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

    // Update is called once per frame
    void Update()
    {
        if(ModeManagerScript.Instance.isRealMode){
            HideUnderTable();
        }
        else {
            // 텍스트가 나오지 않고 해당 단계의 액션이면 숙였는지 확인한다. 
            if (earthquakeStageManager.CurrentStepCount == interactIndex && !earthquakeStageManager.IsAutoStepRunning) {
                HideUnderTable();
            }
        }
        
    }

    public void HideUnderTable()
    {   
        if (hasInteracted) return;
        // 숙였는지 확인한다.
        float currentHeight = hmdTransform.position.y;
        bool isCrouching = currentHeight < standingHeight * 0.85f;

        // 테이블 아래에서 숨겼는지 확인
        bool isUnderTable = tableAreaCollider.bounds.Contains(hmdTransform.position);
        
        if (isCrouching && isUnderTable)
        {
            Debug.Log("숙임");

            if(ModeManagerScript.Instance.isRealMode){
                EarthquakeScoreManager.Instance.CompleteAction(ActionType.Cover);
            }
            else {
                earthquakeStageManager.TriggerStep();
            }
            hasInteracted = true;
            return;
        }
    
    }
}