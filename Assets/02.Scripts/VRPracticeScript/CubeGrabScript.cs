using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrabScript : MonoBehaviour, IInteractable
{
    [SerializeField] private int interactIndex = 0;
    public int InteractIndex => interactIndex;

    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    [SerializeField] private Transform controller;

    public void GrabCube()
    {
        if (hasInteracted)
        {
            return;
        }

        if (controller == null)
        {
            return;
        }

        // 큐브를 컨트롤러의 자식으로 설정 (잡힌 상태)
        transform.SetParent(controller);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // 상호작용 완료 상태로 변경
        hasInteracted = true;
    }
}
