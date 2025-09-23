using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPRInteractionScript : MonoBehaviour, IInteractable
{
    private Vector3 worldPosition;
    private Quaternion worldRotation;
    private Vector3 worldScale = Vector3.one;

    [SerializeField]
    private GameObject CPRUI;
    [SerializeField]
    private ElectricRealManagerScript realGameManager;

    [SerializeField]
    private TextUIManagerScript textManager;

    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void CPRUiOn()
    {
        if (hasInteracted) return;
        if (ModeManagerScript.Instance.isRealMode)
        {
            if (realGameManager.getCompletedActionList[0] == false)
            {
                realGameManager.SetEndState();
                return;
            }
        }
        textManager.DeactiveUIWithText();
        CPRUI.gameObject.SetActive(true);
        hasInteracted = true;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // 켜질 때 현재 월드값을 기준으로 고정 시작
        worldPosition = transform.position;
        worldRotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.position = worldPosition;
        transform.rotation = worldRotation;
    }
}
