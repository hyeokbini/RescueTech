using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void GrabPhone()
    {
        if (hasInteracted) return;
        textManager.IncreaseIndex();
        hasInteracted = true;
        gameManager.IncreaseStageStep();
    }
}
