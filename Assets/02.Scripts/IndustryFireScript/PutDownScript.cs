using UnityEngine;
using UnityEngine.EventSystems;

public class PutDownScript : MonoBehaviour, IInteractable
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private Transform handAnchor;
    [SerializeField] private GameObject fireCover;
    [SerializeField] private Transform originParent;
    private GameObject fireCoverZone;

    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    [SerializeField]
    private MonoBehaviour countScript;
    private IManagerObjCount Count => countScript as IManagerObjCount;

    public int InteractIndex => Count.ObjCount;

    [SerializeField] private FireCoverScript fireCoverScript;
   

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            fireCoverZone = transform.parent.gameObject;
    }

    public void PutDown()
    {
        if (fireCover == null || fireCoverZone == null) return;
        fireCoverScript.isCarrying = false;
        fireCover.transform.SetParent(originParent);

        fireCover.transform.localPosition = new Vector3(850f, -0.71f, 534.5f);
        fireCover.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

        hasInteracted = true;
    }
}
