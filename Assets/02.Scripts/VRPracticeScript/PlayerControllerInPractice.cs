using UnityEngine;
using HTC.UnityPlugin.Vive;

public class InteractionHighlighter : MonoBehaviour
{
    public HandRole hand = HandRole.RightHand;
    public float maxDistance = 10f;

    void Update()
    {
        // 트리거 버튼을 눌렀을 때만 동작
        if (ViveInput.GetPressDown(hand, ControllerButton.Trigger))
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                GameObject hitObject = hit.collider.gameObject;

                // IInteractable 인터페이스가 붙어 있는지 검사
                var interactable = hitObject.GetComponentInChildren<IInteractable>(true);

                if (interactable != null)
                {
                    // InteractionUI 찾기
                    Transform uiTransform = hitObject.transform.Find("InteractionUI");
                    if (uiTransform != null)
                    {
                        var uiHandler = uiTransform.GetComponent<UITurnOnOffController>();
                        if (uiHandler != null)
                        {
                            // 카메라 방향으로 정렬
                            uiTransform.LookAt(uiTransform.position + Camera.main.transform.forward, Camera.main.transform.up);
                            uiHandler.ShowUI();
                        }
                    }
                }
            }
        }
    }
}
