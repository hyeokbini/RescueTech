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
    private GameObject oxygenTank;

    private void Awake()
    {
        oxygenTank = GameObject.Find("OxygenTank Variant");
    }

    public void SupplyOxygen()
    {
        if (hasInteracted) return;
        oxygenTank.gameObject.SetActive(false);
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
