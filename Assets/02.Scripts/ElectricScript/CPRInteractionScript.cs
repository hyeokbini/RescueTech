using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPRInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject CPRUI;

    [SerializeField]
    private TextUIManagerScript textManager;

    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;


    public void CPRUiOn()
    {
        if (hasInteracted) return;
        textManager.DeactiveUIWithText();
        CPRUI.gameObject.SetActive(true);
        hasInteracted = true;
        gameObject.SetActive(false);
    }
}
