using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cushion : MonoBehaviour, IInteractable
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private EarthquakePracticeModeGameManagerScript earthquakeStageManager;
    private GameObject cushion;
    
    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            cushion = transform.parent.gameObject;
    }

    // 방법 1. 하나의 함수에서 if문으로 구분
    public void PutOnHead()
    {   
        // 카메라 자식으로 붙이기
        cushion.transform.SetParent(Camera.main.transform);
        cushion.transform.localPosition = new Vector3(0, 0.25f, 0);
        cushion.transform.localRotation = Quaternion.identity;
        hasInteracted = true;

        // 실전모드라면 가점
        if(ModeManagerScript.Instance.isRealMode){
            EarthquakeScoreManager.Instance.CompleteAction(ActionType.Cushion);
        }
        else {
            // 텍스트가 나오고 있고, 해당 단계의 액션이 아니면 막는다. 
            if (earthquakeStageManager.CurrentStepCount != interactIndex && !earthquakeStageManager.IsAutoStepRunning) {
                Debug.Log(earthquakeStageManager.CurrentStepCount);
                return;
            }
            // 다음 스테이지로 넘기기
            earthquakeStageManager.TriggerStep();
        }
        
    }
}
