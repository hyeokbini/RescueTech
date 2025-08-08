using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyphoonPlayerScript : MonoBehaviour
{
    [SerializeField]
    private InteractionHandler handler;
    [SerializeField]
    private GameObject tape;
    public bool isGrabTape = false;


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
        tape.GetComponent<MeshCollider>().enabled = false;
        isGrabTape = true;
    }
}
