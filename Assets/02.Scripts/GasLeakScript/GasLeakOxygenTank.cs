using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasLeakOxygenTank : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private int interactIndex = 6;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    private GameObject target;
    private GameObject rightHand;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            target = transform.parent.gameObject;

        rightHand = GameObject.Find("RightHand");
    }

    public void Grab()
    {
        // 컨트롤러 자식으로 붙이기
        target.transform.SetParent(rightHand.transform);
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        hasInteracted = true;
        gasPracticeModeManager.IncreaseStageStep();
    }
}
