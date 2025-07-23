using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using HTC.UnityPlugin.Vive;

public class PlayerController : MonoBehaviour
{
    public HandRole hand = HandRole.RightHand;
    public float maxDistance = 10f;
    [SerializeField]
    private MonoBehaviour countScript;
    private IManagerObjCount Count => countScript as IManagerObjCount;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        // 레이캐스트 검사
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            // Ray가 맞은 오브젝트에 UI 인터페이스가 붙어 있는지 검사
            if (hit.collider.TryGetComponent(out IPointerEnterHandler pointer))
            {
                // Unity가 내부적으로 UI 버튼 위에 마우스 올린 것처럼 처리 (hover 등)
            }


            // 트리거를 눌렀으면
            if (ViveInput.GetPressDown(hand, ControllerButton.Trigger))
            {
                GameObject hitObject = hit.collider.gameObject;
                // UI 버튼이면 클릭만 처리
                if (hitObject.GetComponent<Button>() != null)
                {
                    Debug.Log("버튼 누름");
                    var pointerEvent = new PointerEventData(EventSystem.current)
                    {
                        position = Camera.main.WorldToScreenPoint(hit.point)
                    };
                    ExecuteEvents.Execute(hitObject, pointerEvent, ExecuteEvents.pointerClickHandler);
                }
                else
                {
                    Debug.Log("오브젝트 누름");
                    Debug.Log(hitObject);
                    var interactable = hitObject.GetComponentInChildren<IInteractable>(true);
                    if (interactable == null)
                    {
                        Debug.Log("오류");
                    }
                    if (interactable != null && interactable.HasInteracted)
                    {
                        Debug.Log("이미 상호작용된 오브젝트");
                        return;
                    }
                    if (Count.ObjCount != interactable.InteractIndex && !ModeManagerScript.Instance.isRealMode)
                    {
                        Debug.Log("순서 잘못됨");
                        return;
                    }
                    // UI 활성화
                        Transform uiTransform = hitObject.transform.Find($"InteractionUI");
                    Debug.Log(uiTransform);
                    if (uiTransform != null)
                    {
                        var uiHandler = uiTransform.GetComponent<UITurnOnOffController>();
                        if (uiHandler != null)
                        {
                            uiTransform.LookAt(uiTransform.position + Camera.main.transform.forward, Camera.main.transform.up);
                            uiHandler.ShowUI();
                        }
                    }
                    else
                    {
                        // UI가 없는 경우 아무 동작 하지 않음
                    }

                }
            }
        }
    }
}
