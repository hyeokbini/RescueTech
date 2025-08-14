using UnityEngine;
using Valve.VR; // SteamVR Input 사용을 위해 필요
using HTC.UnityPlugin.Vive; // Vive Input Utility 사용을 위해 필요

public class MainPlayerMovement : MonoBehaviour
{
    public float gravity = -9.81f;
    public float terminalVelocity = -20.0f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private CharacterController _characterController;
    private Vector3 _verticalVelocity;

    private Transform _hmdTransform; // HMD 시점 (Camera)

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            Debug.LogError("Player 오브젝트에 CharacterController가 없습니다!");
            enabled = false;
            return;
        }

        // ViveCameraRig 자식으로부터 Camera 찾기
        var cameraGO = GameObject.Find("ViveCameraRig/Camera");
        if (cameraGO != null)
        {
            _hmdTransform = cameraGO.transform;
        }
        else
        {
            Debug.LogError("ViveCameraRig의 자식으로 Camera를 찾을 수 없습니다. 경로 및 이름을 확인하세요.");
            enabled = false;
            return;
        }

        if (groundLayer.value == 0)
        {
            Debug.LogWarning("Ground Layer가 설정되지 않았습니다. PlayerMovement 스크립트의 Inspector에서 Ground Layer를 설정해주세요.");
        }
    }

    void Update()
    {
        if (_characterController == null || _hmdTransform == null)
        {
            return;
        }

        // --- 중력 적용 ---
        bool isGrounded = IsGrounded();
        if (isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = -2f;
        }

        _verticalVelocity.y += gravity * Time.deltaTime;
        _verticalVelocity.y = Mathf.Max(_verticalVelocity.y, terminalVelocity);

        // ✅ HMD(Camera) 기준 방향 사용
        Vector3 forwardDirection = _hmdTransform.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();

        Vector3 rightDirection = _hmdTransform.right;
        rightDirection.y = 0;
        rightDirection.Normalize();
    }

    private bool IsGrounded()
    {
        float radius = _characterController.radius;
        float height = _characterController.height;
        Vector3 center = _characterController.center;

        Vector3 sphereOrigin = transform.position + center - new Vector3(0, height / 2 - radius, 0);

        return Physics.CheckSphere(sphereOrigin, radius, groundLayer);
    }
}
