using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyphoonPlayerScript : MonoBehaviour
{
    [SerializeField]
    private InteractionHandler handler;
    [SerializeField]
    private bool isGrabTape = false;

    private void OnEnable()
    {
        handler.grabTape += SetGrabTape;
    }

    private void OnDisable()
    {
        handler.grabTape -= SetGrabTape;
    }

    private void SetGrabTape()
    {
        isGrabTape = true;
    }
}
