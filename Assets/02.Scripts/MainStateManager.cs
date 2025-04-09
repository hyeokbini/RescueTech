using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject clearUIPanel;

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
