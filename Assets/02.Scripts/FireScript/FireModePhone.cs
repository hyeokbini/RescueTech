using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModePhone : MonoBehaviour, IInteractable
{
    [SerializeField]
    private FirePracticeModeManager firePracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private GameObject phoneUI;
    [SerializeField]
    private int interactIndex = 1;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    void Start()
    {
        phoneUI.SetActive(false);
    }

    public void TurnOnPhone()
    {
        if (hasInteracted) return;
        phoneUI.SetActive(true);
    }

    public void CallTo119()
    {
        textUIManager.IncreaseIndex();
        textUIManager.ActivateUIWithText();
        hasInteracted = true;
        firePracticeModeManager.IncreaseStageStep();
        phoneUI.SetActive(false);
    }
}
