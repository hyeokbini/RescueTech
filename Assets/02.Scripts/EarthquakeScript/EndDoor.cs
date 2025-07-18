using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour, IInteractable
{
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;


    [SerializeField] private EarthquakePracticeModeGameManagerScript earthquakeStageManager;

    [SerializeField]
    private MonoBehaviour countScript;
    private IManagerObjCount Count => countScript as IManagerObjCount;

    public int InteractIndex => Count.ObjCount;


    public void endDoorOpen()
    {   
        // 다음 스테이지로 넘기기
        earthquakeStageManager.TriggerStep();
        hasInteracted = true;
    }
}
