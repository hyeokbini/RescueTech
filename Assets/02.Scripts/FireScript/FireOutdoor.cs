using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOutdoor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private FirePracticeModeManager firePracticeModeManager;
    [SerializeField]
    private int interactIndex = 5;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void Out()
    {
        if (hasInteracted) return;
        if(ModeManagerScript.Instance.isRealMode)
        {
            FireScoreManager.Instance.CompleteAction(FireAction.OutDoor);
            FireRealModeFlowManager.Instance.EndStage();
            hasInteracted = true;
        }
        else
        {
            hasInteracted = true;
            firePracticeModeManager.IncreaseStageStep();
        }
    }
}
