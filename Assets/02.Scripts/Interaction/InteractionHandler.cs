using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    private GameObject target;
    private Animator animator;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            target = transform.parent.gameObject;

        animator = target.GetComponent<Animator>();
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
        animator.SetTrigger("Open");
    }
}
