using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyphoonPracticeModeManagerScript : MonoBehaviour, IManagerObjCount
{
    [SerializeField]
    private int stageStepCount; // 스테이지에서 진행해야 할 총 단계
    public int currentStepCount = 0; // 현재 단계
    [SerializeField]
    private GameObject clearUIPanel; // ui 오브젝트 연결
    [SerializeField]
    private TextUIManagerScript textManager;
    private Coroutine endingCoroutine;
    public int ObjCount => currentStepCount;

    private void Start()
    {
        if (ModeManagerScript.Instance.isRealMode)
        {
            gameObject.SetActive(false);
            return;
        }
        textManager.ActivateUIWithText();
    }

    // 각 오브젝트에 gamemanager를 참조해서 이 함수 실행
    public void IncreaseStageStep()
    {
        currentStepCount++;
        // 스테이지 클리어 로직 체크
        if (currentStepCount == stageStepCount)
        {
            StageClear();
        }
    }

    // 스테이지 클리어 로직
    private void StageClear()
    {
        if (endingCoroutine != null)
        {
            return;
        }
        textManager.DeactiveUIWithText();
        endingCoroutine = StartCoroutine(EndingCoroutine());
    }

    IEnumerator EndingCoroutine()
    {
        yield return StartCoroutine(GetComponent<TyphoonEndingFadeInOutScript>().FadeCoroutine());
        textManager.ActivateUIWithText();
        endingCoroutine = null;
    }

    // 임시 로직 테스트용 코드
    /**
    private void onclick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            IncreaseStageStep();
        }
    }

    void Update()
    {
        onclick();
    }
    */
}