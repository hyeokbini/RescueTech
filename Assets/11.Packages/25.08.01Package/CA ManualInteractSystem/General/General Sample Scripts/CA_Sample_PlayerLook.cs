using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Sample_PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 1;
    public Transform playerBody;

    float xClamp;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xClamp = 0;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xClamp += mouseY;

        if (xClamp > 90)
        {
            xClamp = 90;
            mouseY = 0;
            ClampXAxisRotationToValue(270);
        }

        else if (xClamp < -90)
        {
            xClamp = -90;
            mouseY = 0;
            ClampXAxisRotationToValue(90);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
