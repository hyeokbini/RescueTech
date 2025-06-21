using UnityEngine;
using HTC.UnityPlugin.Vive;

public class PhoneInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject phoneUI;     //휴대폰 UI

    private Transform rightHandTransform;   //오른손 컨트롤러의 transform

    void Start()
    {
        //오른손 컨트롤러 찾기
        var rightHand = GameObject.Find("ViveCameraRig/RightHand");
        if (rightHand != null)
        {
            rightHandTransform = rightHand.transform;
        }
        else
        {
            Debug.LogError("ViveCameraRig의 자식으로 RightHand를 찾을 수 없습니다. 경로 및 이름을 확인하세요.");
            enabled = false;
            return;
        }
        //휴대폰 UI null 체크 및 비활성화
        if(!phoneUI)
        {
            Debug.LogError("No Phone UI");
            return;
        }

        phoneUI.SetActive(false);
    }

    void Update()
    {
        //오른손 컨트롤러의 버튼을 누르면 컨트롤러 위치의 앞 방향으로 Raycast
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger))
        {
            Ray ray = new Ray(rightHandTransform.position, rightHandTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                //Raycast해서 충돌한 오브젝트의 태그가 Phone이면 휴대폰 UI 활성화
                if (hit.collider.CompareTag("Phone"))
                {
                    phoneUI.SetActive(true);
                }
            }
        }
    }
}
