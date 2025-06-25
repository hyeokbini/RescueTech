using UnityEngine;
using HTC.UnityPlugin.Vive;

public class PlayerGrab : MonoBehaviour
{
    public HandRole hand = HandRole.RightHand;
    public float maxDistance = 10f;

    private GameObject grabbedObject;
    private Rigidbody grabbedRb;

    void Update()
    {
        // 트리거를 눌렀고, 아직 잡은 게 없으면
        if (grabbedObject == null && ViveInput.GetPressDown(hand, ControllerButton.Trigger))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            // Ray에 맞은 오브젝트 잡기
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                Grab(hit.collider.gameObject);
            }
        }
    }

    void Grab(GameObject obj)
    {
        grabbedObject = obj;
        grabbedRb = obj.GetComponent<Rigidbody>();
        if (grabbedRb != null) grabbedRb.isKinematic = true;
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
}
