using UnityEngine;
using System.Collections;

public class InteractionHandler : MonoBehaviour
{
    private GameObject target;
    private Animator animator;
    public float angle;
    public float speed;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            target = transform.parent.gameObject;
    }

    public void Grab()
    {
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
