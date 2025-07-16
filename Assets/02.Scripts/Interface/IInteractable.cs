using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    int InteractIndex { get; }
    bool HasInteracted { get; }
}
