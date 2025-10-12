using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private GameObject door;
    
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    

    [SerializeField]
    private MonoBehaviour countScript;
    private IManagerObjCount Count => countScript as IManagerObjCount;

    public int InteractIndex => Count.ObjCount;

    [SerializeField]
    private float targetAngle = -45f;

    [SerializeField]
    private bool isStage = false;
    [SerializeField] private EarthquakePracticeModeGameManagerScript earthquakeStageManager;

    private Coroutine RotateCoroutine;

    [SerializeField] private AudioSource doorSource;
    [SerializeField] private AudioClip doorClip;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            door = transform.parent.gameObject;   
    }

    public void Open()
    {
        doorSource.PlayOneShot(doorClip, 1f);
        if (isStage == false) {
            if(RotateCoroutine == null)
                RotateCoroutine = StartCoroutine(RotateY(door, targetAngle, 130f));
            hasInteracted = true;
        } else {
            if (earthquakeStageManager.CurrentStepCount == 8 && !earthquakeStageManager.IsAutoStepRunning) {
                if(RotateCoroutine == null)
                RotateCoroutine = StartCoroutine(RotateY(door, targetAngle, 130f));
                hasInteracted = true;
            }
        }
        
    }
    public void Open_R()
    {
        doorSource.PlayOneShot(doorClip, 1f);
        if(RotateCoroutine == null){
                RotateCoroutine = StartCoroutine(RotateY(door, targetAngle, 130f));
                hasInteracted = true;
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
            RotateCoroutine = null;
            yield return null;
        }
    }
}
