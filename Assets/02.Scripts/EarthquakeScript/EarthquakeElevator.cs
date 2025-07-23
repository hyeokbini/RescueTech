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

    [SerializeField]
    private GameObject UI_elevator;

    private Coroutine activePannelCoroutine;

    private void Awake()
    {
        // 이 스크립트 붙은 오브젝트의 부모를 target으로 설정
        if (transform.parent != null)
            elevator = transform.parent.gameObject;   
    }

    public void TakeElevator()
    {
        hasInteracted = true;
        // 감점
        EarthquakeScoreManager.Instance.CompleteAction(ActionType.Elevator);

        // 안내 문구 표시

        if (activePannelCoroutine != null)
        {
            StopCoroutine(activePannelCoroutine);
        }
        activePannelCoroutine = StartCoroutine(ActivePannelCoroutine());
    }

    IEnumerator ActivePannelCoroutine()
    {
        UI_elevator.SetActive(true);
        // 3초 대기
        yield return new WaitForSeconds(3f);
        UI_elevator.SetActive(false);
        activePannelCoroutine = null;
    }

}
