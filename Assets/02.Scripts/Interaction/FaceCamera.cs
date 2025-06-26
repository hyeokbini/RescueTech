using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        Vector3 lookTarget = transform.position + Camera.main.transform.forward;
        transform.LookAt(lookTarget, Camera.main.transform.up);
    }
}
