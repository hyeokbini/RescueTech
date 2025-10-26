using UnityEngine;
using UnityEngine.EventSystems;

public class FireCoverScript : MonoBehaviour, IInteractable
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private Transform handAnchor;
    private GameObject fireCover;

    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    [SerializeField]
    private MonoBehaviour countScript;
    private IManagerObjCount Count => countScript as IManagerObjCount;

    public int InteractIndex => Count.ObjCount;

    public bool isCarrying = false;
   

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            fireCover = transform.parent.gameObject;
    }

    public void StartCarry()
    {
        // 오른손 자식으로 붙이기
        fireCover.transform.SetParent(handAnchor);
        fireCover.transform.localPosition = new Vector3(0.95f, -1.63f, 0);
        hasInteracted = true;
        isCarrying = true;
    }
}
