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
    }

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

    void ShowIntro()
    {

    }

    void ShowTutorial()
    {

    }

    void ShowModeBtn()
    {
        
    }

    //자연 및 산업은 Category로 명명
    void ShowCategoryBtn()
    {

    }

    //각 재난은 Situation으로 명명
    void ShowSituationBtn()
    {

    }
}
