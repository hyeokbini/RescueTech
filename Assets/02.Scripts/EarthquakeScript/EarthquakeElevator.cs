using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeElevator : MonoBehaviour, IInteractable
{
    private GameObject elevator;
    
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    [SerializeField]
    private int interactIndex = -1;
    public int InteractIndex => interactIndex;

    [SerializeField] private ElevatorUI elevatorUI;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            elevator = transform.parent.gameObject;   
    }

    // 엘레베이터 클릭 시 호출
    public void TakeElevator()
    {
        hasInteracted = true;
        // 감점
        EarthquakeScoreManager.Instance.CompleteAction(ActionType.Elevator);

        elevatorUI.showUI();
    }

}
