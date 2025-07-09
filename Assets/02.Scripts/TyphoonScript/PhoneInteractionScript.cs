using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TextUIManagerScript textManager;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void GrabPhone()
    {
        if (hasInteracted) return;
        textManager.IncreaseIndex();
        hasInteracted = true;
        gameManager.IncreaseStageStep();
    }
}
