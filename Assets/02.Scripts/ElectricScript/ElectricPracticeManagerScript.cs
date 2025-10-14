using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElectricPracticeManagerScript : MonoBehaviour, IManagerObjCount
{
    [SerializeField]
    private int stageStepCount; // 스테이지에서 진행해야 할 총 단계
    public int currentStepCount = 0; // 현재 단계
    [SerializeField]
    private GameObject clearUIPanel; // ui 오브젝트 연결
    [SerializeField]
    private TextMeshProUGUI clearTextcomponent;
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

    public void SetFeedBack()
    {
        clearUIPanel.SetActive(true);
        clearTextcomponent.text = "연습 모드가 종료되었습니다.\n\n실제 상황에서는 119가 언제 도착할 지 모르기 때문에\n\n계속해서 심폐소생술을 실시해주어야 합니다.\n\n그립 버튼으로 메인 씬으로 돌아가기";
    }

    IEnumerator EndingCoroutine()
    {
        yield return StartCoroutine(GetComponent<ElectricEndingFadeInOutScript>().FadeCoroutine());
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
