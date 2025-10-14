using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Vector3 firstTransform;

    void Awake()
    {
        firstTransform = transform.position;
        Debug.Log(gameObject.name + " " + firstTransform);
    }

    void LateUpdate()
    {
        transform.position = firstTransform;

        if (Camera.main == null) return;

        Vector3 lookTarget = transform.position + Camera.main.transform.forward;
        transform.LookAt(lookTarget, Camera.main.transform.up);
    }
}
