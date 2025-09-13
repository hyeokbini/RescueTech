using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskGlove : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    [SerializeField]
    private GasWearType gasWearType;
    [SerializeField]
    GasWear gasWear;
    public void Wear()
    {
        if (hasInteracted) return;
        hasInteracted = true;
        gasWear.Check(gasWearType);
    }
}
