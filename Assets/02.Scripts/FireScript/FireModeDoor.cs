using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModeDoor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private FirePracticeModeManager firePracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float targetAngle = -45f;
    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    private Coroutine rotateCoroutine;

    public void Open()
    {
        if (hasInteracted) return;
        if(rotateCoroutine == null)
            rotateCoroutine = StartCoroutine(RotateY(door, targetAngle, 130f));
        if(ModeManagerScript.Instance.isRealMode)
        {
            FireScoreManager.Instance.CompleteAction(FireAction.Stair);
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
            rotateCoroutine = null;
            yield return null;
        }
    }
}
