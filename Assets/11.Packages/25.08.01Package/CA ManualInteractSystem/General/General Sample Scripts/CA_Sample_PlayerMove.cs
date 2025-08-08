using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Sample_PlayerMove : MonoBehaviour
{
    public float moveSpd = 10;
    CharacterController cc;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal") * moveSpd * Time.deltaTime;
        float vInput = Input.GetAxis("Vertical") * moveSpd * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * vInput;
        Vector3 rightMovement = transform.right * hInput;

        cc.SimpleMove(forwardMovement + rightMovement);
    }
}
