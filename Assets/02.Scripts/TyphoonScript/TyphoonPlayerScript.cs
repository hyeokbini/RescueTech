using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyphoonPlayerScript : MonoBehaviour
{
    [SerializeField]
    private InteractionHandler handler;
    [SerializeField]
    private GameObject tape;
    public Vector3 startPosition;
    public bool isGrabTape = false;

    private void Awake()
    {
        startPosition = gameObject.transform.position;
    }

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

    public void EndState()
    {
        gameObject.GetComponent<PlayerMovement>().gravity = 0;
        gameObject.transform.position = startPosition;
    }
}
