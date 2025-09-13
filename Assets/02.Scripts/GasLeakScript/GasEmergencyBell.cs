using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasEmergencyBell : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private int interactIndex = 0;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    public void Ring()
    {
        if (hasInteracted) return;
        if (ModeManagerScript.Instance.isRealMode)
        {

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
