using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasWalkieTalkie : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private int interactIndex = 1;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void Call()
    {
        if (hasInteracted) return;
        if(ModeManagerScript.Instance.isRealMode)
        {
            GasLeakScoreManager.Instance.CompleteAction(GasAction.WalkieTalkie);
            hasInteracted = true;
        }
        else
        {
            textUIManager.IncreaseIndex();
            textUIManager.ActivateUIWithText();
            hasInteracted = true;
            gasPracticeModeManager.IncreaseStageStep();
        }
    }
}
