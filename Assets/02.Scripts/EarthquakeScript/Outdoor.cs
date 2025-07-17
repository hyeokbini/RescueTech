using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outdoor : MonoBehaviour
{
    [SerializeField] private TextUIManagerScript textUIManager;
    [SerializeField] private EarthquakePracticeModeGameManagerScript earthquakeStageManager;
    [SerializeField] private Transform hmdTransform;
    [SerializeField] private Collider outdoorAreaCollider;

    [SerializeField]
    private int interactIndex = 8;
    public int InteractIndex => interactIndex;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    
    private float standingHeight;

    private void Start()
    {
        standingHeight = hmdTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInteracted) return;
        bool isOutdoor = outdoorAreaCollider.bounds.Contains(hmdTransform.position);
        if (isOutdoor && earthquakeStageManager.CurrentStepCount == interactIndex){
            earthquakeStageManager.TriggerStep();
            hasInteracted = true;
        }

    }

}