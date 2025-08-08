using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towel : MonoBehaviour, IInteractable
{
    [SerializeField]
    private FirePracticeModeManager firePracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private GameObject towel;
    [SerializeField]
    private int interactIndex = 2;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void PutOnFace()
    {
        if (hasInteracted) return;
        // 카메라 자식으로 붙이기
        towel.transform.SetParent(Camera.main.transform);
        towel.transform.localPosition = new Vector3(0,-0.07f,0.1f);
        towel.transform.localRotation = Quaternion.Euler(0, -90f, -90f);
        if(ModeManagerScript.Instance.isRealMode)
        {
            FireScoreManager.Instance.CompleteAction(FireAction.Towel);
            hasInteracted = true;
        }
        else
        {
            textUIManager.IncreaseIndex();
            textUIManager.ActivateUIWithText();
            hasInteracted = true;
            firePracticeModeManager.IncreaseStageStep();
        }
    }

    public void PutOffFace()
    {
        towel.SetActive(false);
    }
}
