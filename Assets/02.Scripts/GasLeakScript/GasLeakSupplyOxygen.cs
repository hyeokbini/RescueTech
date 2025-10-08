using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasLeakSupplyOxygen : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private int interactIndex = 7;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    [SerializeField]
    private GasLeakOxygenTank oxygenTank;

    public void SupplyOxygen()
    {
        if (hasInteracted || !oxygenTank.isGrab) return;
        oxygenTank.UnGrab();
        if (ModeManagerScript.Instance.isRealMode)
        {
            GasLeakScoreManager.Instance.CompleteAction(GasAction.OxygenTank);
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
