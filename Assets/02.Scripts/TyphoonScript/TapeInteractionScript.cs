using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int interactIndex = 1;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
}
