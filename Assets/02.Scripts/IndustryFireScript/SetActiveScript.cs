using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour, IInteractable
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private GameObject target;


    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    [SerializeField]
    private MonoBehaviour countScript;
    private IManagerObjCount Count => countScript as IManagerObjCount;

    public int InteractIndex => Count.ObjCount;
    

    private void Awake()
    {
        /*
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            target = transform.parent.gameObject;*/
    }

    public void AddItem()
    {   
        // 실전모드라면 가점
        /*
        if(ModeManagerScript.Instance.isRealMode){
            EarthquakeScoreManager.Instance.CompleteAction(ActionType.Cushion);
        }*/

        // 아이템 활성화 시키기
        if (target != null)
        {
            target.SetActive(true);
        }

    }
}
