using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField] 
    private FireAction fatalAction = FireAction.Fire; 

    private void OnTriggerEnter(Collider other)
    {
        if(!ModeManagerScript.Instance.isRealMode) return;
        Debug.Log($"[FireZone] TriggerEnter by: {other.name}, tag: {other.tag}");
        // 치명적 실패 기록
        FireScoreManager.Instance.CompleteAction(fatalAction, true);
        // 게임 종료
        FireRealModeFlowManager.Instance.EndStage();
    }
}
