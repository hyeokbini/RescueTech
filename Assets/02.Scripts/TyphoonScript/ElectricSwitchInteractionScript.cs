using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSwitchInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private GameObject offSwitch;
    [SerializeField]
    private GameObject onSwitch;
    [SerializeField]
    private int interactIndex = 2;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    public void TurnOnSwitch()
    {
        if (hasInteracted) return;
        textManager.IncreaseIndex();
        textManager.ActivateUIWithText();
        hasInteracted = true;
        gameManager.IncreaseStageStep();
        offSwitch.gameObject.SetActive(false);
        onSwitch.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
