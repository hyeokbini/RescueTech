using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MainState
{
    Intro,
    Tutorial,
    SelectMode,
    SelectCategory,
    SelectSituation
}

public class MainStateManager : MonoBehaviour
{
    
    private MainState currentState;
    private float timer = 0f;
    private bool introTimer = false;
    private bool isRealMode = true;
    private bool isNatural = true;

    [SerializeField]
    private GameObject UI_intro;
    [SerializeField]
    private GameObject UI_tutorial;
    [SerializeField]
    private Button tutorialYesBtn;
    [SerializeField]
    private Button tutorialNoBtn;
    [SerializeField]
    private GameObject UI_mode;
    [SerializeField]
    private Button realModeBtn;
    [SerializeField]
    private Button practicelModeBtn;
    [SerializeField]
    private GameObject UI_category;
    [SerializeField]
    private Button industryBtn;
    [SerializeField]
    private Button naturalBtn;
    [SerializeField]
    private GameObject UI_naturalSituation;
    [SerializeField]
    private Button fireNaturalBtn;
    [SerializeField]
    private Button typhoonBtn;
    [SerializeField]
    private Button earthquakeBtn;
    [SerializeField]
    private GameObject UI_industrySituation;
    [SerializeField]
    private Button fireIndustryBtn;
    [SerializeField]
    private Button electronicBtn;
    [SerializeField]
    private Button gasBtn;

    void Start()
    {
        SetState(MainState.Intro);
        introTimer = true;
    }

    // 인트로를 2초동안 보여주기 위해 시간을 센다.
    void Update()
    {
        if (introTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                // 2초 후 인트로 내리기
                SetState(MainState.Tutorial);
                introTimer = false;
            }
        }
    }

    // 상태를 전이하는 메서드
    void SetState(MainState state)
    {
        currentState = state;

        switch (currentState)
        {
            case MainState.Intro:
                ShowIntro();
                break;
            case MainState.Tutorial:
                ShowTutorial();
                break;
            case MainState.SelectMode:
                ShowModeBtn();
                break;
            case MainState.SelectCategory:
                ShowCategoryBtn();
                break;
            case MainState.SelectSituation:
                ShowSituationBtn();
                break;
        }
    }
    // 씬 전환 메서드
    public void SceneChange()
    {
        Debug.Log("씬 전환");
    }

    // 인트로를 보여주는 메서드
    public void ShowIntro()
    {
        Debug.Log("프로그램 실행");
    }

    // 튜토리얼 선택창을 보여주는 메서드
    public void ShowTutorial()
    {
        Debug.Log("사용자 경험 체크");
        UI_intro.SetActive(false);
        UI_tutorial.SetActive(true);
    }

    // 모드 선택창을 보여주는 메서드
    public void ShowModeBtn()
    {
        Debug.Log("모드 선택 체크");
        UI_tutorial.SetActive(false);
        UI_mode.SetActive(true);
    }

    // 모드 확인 메서드
    public void ChangeMode()
    {
        isRealMode = !isRealMode;
        Debug.Log("연습 모드 선택");
    }

    // 자연 및 산업은 Category로 명명
    // 카테고리 선택창을 보여주는 메서드
    public void ShowCategoryBtn()
    {
        Debug.Log("카테고리 선택 체크");
        UI_mode.SetActive(false);
        UI_category.SetActive(true);

    }

    // 카테고리 확인 메서드
    public void ChangeCategory()
    {
        isNatural = !isNatural;
        Debug.Log("산업 재난 선택");
    }

    //각 재난은 Situation으로 명명
    // 재난 상황 선택창을 보여주는 메서드
    public void ShowSituationBtn()
    {
        if (isRealMode)
        {
            // isNatural 로 산업 랜덤 혹은 자연 랜덤 씬 부르기
            Debug.Log("랜덤 씬 변환");
        }
        else
        {
            if (isNatural)
            {
                Debug.Log("자연 재난 상황 선택");
                UI_category.SetActive(false);
                UI_naturalSituation.SetActive(true);
            }
            else 
            {
                Debug.Log("산업 재난 상황 선택");
                UI_category.SetActive(false);
                UI_industrySituation.SetActive(true);
            }
        }
        

    }
}
