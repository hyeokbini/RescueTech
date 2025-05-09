using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;         //캐릭터 컨트롤러 컴포넌트
    [SerializeField]
    private Camera playerCamera;                    //카메라 컴포넌트
    [SerializeField]
    private float moveSpeed = 5f;                   //이동 속도
    [SerializeField]
    private float mouseSensitivity = 5f;            //마우스 속도
    private Vector3 velocity;                       //속도
    private float gravity = -9.81f;                 //중력
    private float rotationX = 0f;                   //x축 회전값
    private float rotationY = 0f;                   //y축 회전값

    void Update()
    {
        //마우스 회전
        float mouseX = Input.GetAxis("Mouse X");    //마우스 x
        float mouseY = Input.GetAxis("Mouse Y");    //마우스 y
        rotationY += mouseX * mouseSensitivity;     //각 회전값 = 마우스 입력값 * 마우스 속도
        rotationX += mouseY * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -35f, 35f);          //x축 회전 -35~35 제한
        transform.eulerAngles = new Vector3(0f, rotationY, 0f); //몸체 회전
        playerCamera.transform.rotation = Quaternion.Euler(-rotationX, rotationY, 0f); //카메라 회전

        //이동
        float h = Input.GetAxis("Horizontal");  //수평 방향 키보드 입력
        float v = Input.GetAxis("Vertical");    //수직 방향 키보드 입력
        Vector3 move = transform.right * h + transform.forward * v; //방향을 포함한 이동 벡터
        //중력 적용
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;  //바닥에 붙어 있도록 유지
        velocity.y += gravity * Time.deltaTime;
        controller.Move((move * moveSpeed + velocity) * Time.deltaTime);         //컨트롤러에 적용
    }
}
