using UnityEngine;
using System.Collections;

public class InteractionHandler : MonoBehaviour
{
    private GameObject target;
    private GameObject rightHand;
    private Animator animator;
    public float angle;
    public float speed;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            target = transform.parent.gameObject;
        
        rightHand = GameObject.Find("RightHand");
    }

    // 잡기 클릭 시 단순히 오른쪽 컨트롤러 위에 배치됨
    // 제자리 두기는 아직 구현되지 않음
    public void Grab()
    {
        // 컨트롤러 자식으로 붙이기
        target.transform.SetParent(rightHand.transform);
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.identity;
    }

    public void PutOnHead()
    {
        target.transform.SetParent(Camera.main.transform);
        target.transform.localPosition = new Vector3(0, 0.3f, 0);
        target.transform.localRotation = Quaternion.identity;
    }

    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(RotateY(target, angle, speed));
    }

    private IEnumerator RotateY(GameObject obj, float targetAngle, float speed)
    {
        float rotated = 0f;
        float direction = Mathf.Sign(targetAngle); // 회전 방향

        while (Mathf.Abs(rotated) < Mathf.Abs(targetAngle))
        {
            float delta = speed * Time.deltaTime;
            if (Mathf.Abs(rotated + delta) > Mathf.Abs(targetAngle))
            {
                delta = targetAngle - rotated; 
            }

            obj.transform.Rotate(0f, delta * direction, 0f);
            rotated += delta * direction;

            yield return null;
        }
    }
}
