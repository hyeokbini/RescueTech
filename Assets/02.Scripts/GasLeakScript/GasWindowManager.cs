using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasWindowManager : MonoBehaviour
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private GasInfected gasInfected;
    private bool anyWindowOpened = false;

    public void WindowOpened()
    {
        if (anyWindowOpened) return;
        anyWindowOpened = true;
        if (ModeManagerScript.Instance.isRealMode)
        {
            GasLeakScoreManager.Instance.CompleteAction(GasAction.Window);
        }
        else
        {
            textUIManager.IncreaseIndex();
            textUIManager.ActivateUIWithText();
            gasPracticeModeManager.IncreaseStageStep();
            gasInfected.Infected();
        }
    }
}
