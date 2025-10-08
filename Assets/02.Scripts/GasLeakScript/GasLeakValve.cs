using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasLeakValve : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private int interactIndex = 2;
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private float raiseDistance = 2f;
    [SerializeField]
    private float raiseDuration = 3.0f;
    [SerializeField]
    private GameObject gas;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    private Coroutine wallRaiseCoroutine;
    public void Operate()
    {
        if (hasInteracted) return;
        if(wallRaiseCoroutine == null)
            wallRaiseCoroutine = StartCoroutine(Raise(wall, raiseDistance, raiseDuration));
        gas.SetActive(false);
        if (ModeManagerScript.Instance.isRealMode)
        {
            GasLeakScoreManager.Instance.CompleteAction(GasAction.Valve);
            hasInteracted = true;
        }
        else
        {
            textUIManager.IncreaseIndex();
            textUIManager.ActivateUIWithText();
            hasInteracted = true;
            gasPracticeModeManager.IncreaseStageStep();
        }
    }
    
    private IEnumerator Raise(GameObject obj, float distance, float duration)
    {
        Transform t = obj.transform;
        Vector3 endPosition = t.position + new Vector3(0, distance, 0);
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            t.position = Vector3.Lerp(t.position, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = endPosition;
        wallRaiseCoroutine = null;
    }
}
