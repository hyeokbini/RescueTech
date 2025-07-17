using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasValve : MonoBehaviour, IInteractable
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private EarthquakePracticeModeGameManagerScript earthquakeStageManager;
    private GameObject valve;
    
    [SerializeField]
    private int interactIndex = 4;
    public int InteractIndex => interactIndex;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    [SerializeField]
    private float targetAngle = 60f;

    private Coroutine RotateCoroutine;

    private void Awake()
    {
        if (transform.parent != null)
            valve = transform.parent.gameObject;
    }

    public void TurnValveOn()
    {
        if (earthquakeStageManager.CurrentStepCount != interactIndex || 
            earthquakeStageManager.IsAutoStepRunning || 
            hasInteracted) return;

        StartCoroutine(RotateAndAdvance());
        earthquakeStageManager.TriggerStep();
        hasInteracted = true;
    }

    private IEnumerator RotateAndAdvance()
    {
        // 회전 코루틴 실행
        yield return StartCoroutine(RotateY(valve, targetAngle, 30f));
    }

    private IEnumerator RotateY(GameObject obj, float targetAngle, float speed)
    {
        float rotated = 0f;
        float direction = Mathf.Sign(targetAngle);

        while (Mathf.Abs(rotated) < Mathf.Abs(targetAngle))
        {
            float delta = speed * Time.deltaTime;
            if (Mathf.Abs(rotated + delta) > Mathf.Abs(targetAngle))
                delta = Mathf.Abs(targetAngle) - Mathf.Abs(rotated);

            obj.transform.Rotate(delta * direction, 0f, 0f, Space.Self);
            rotated += delta * direction;

            yield return null;
        }
    }

}
