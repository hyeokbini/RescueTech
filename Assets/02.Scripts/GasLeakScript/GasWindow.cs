using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasWindow : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int interactIndex = 4;
    [SerializeField]
    private GasWindowManager gasWindowManager;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void Open()
    {
        if (hasInteracted) return;
        hasInteracted = true;
        gasWindowManager.WindowOpened();
    }
}
