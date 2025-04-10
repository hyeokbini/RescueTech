using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainStateManager : MonoBehaviour
{
    private bool isRealMode = true;
    private bool isNatural = true;

    [SerializeField]
    private GameObject UI_intro;
    [SerializeField]
    private GameObject UI_tutorial;
    [SerializeField]
    private GameObject UI_mode;
    [SerializeField]
    private GameObject UI_category;
    [SerializeField]
    private GameObject UI_naturalSituation;
    [SerializeField]
    private GameObject UI_industrySituation;

    void Start()
    {
        StartCoroutine(ShowIntro(2f));
    }

    IEnumerator ShowIntro(float delay)
    {
        UI_intro.SetActive(true);
        yield return new WaitForSeconds(delay);
        ShowTutorial();
    }

    
    // 씬 전환 메서드
    public void SceneChange()
    {
        Debug.Log("씬 전환");
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
<<<<<<< HEAD
    }

    // 모드 확인 메서드
    public void SetPracticeMode()
    {
        isRealMode = false;
        Debug.Log("연습 모드 선택");
    }

    public void SetRealMode()
    {
        isRealMode = true;
        Debug.Log("실전 모드 선택");
=======
        practiceModeBtn.onClick.AddListener(() => {
            Debug.Log("연습 모드 선택");
            isRealMode = false;
            UI_mode.SetActive(false);
            SetState(MainState.SelectCategory);
        });
        realModeBtn.onClick.AddListener(() => {
            Debug.Log("실전 모드 선택");
            isRealMode = true;
            UI_mode.SetActive(false);
            SetState(MainState.SelectCategory);
        });
>>>>>>> 288fd3f84a9fa0950f43ea678e71d865b1d538ae
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
    public void SetIndustry()
    {
        isNatural = false;
        Debug.Log("산업 재난 선택");
    }

    public void SetNatural()
    {
        isNatural = true;
        Debug.Log("자연 재난 선택");
    }

    //각 재난은 Situation으로 명명
    // 재난 상황 선택창을 보여주는 메서드
    public void ShowSituationBtn()
    {
        if (isRealMode)
        {
            // isNatural 로 산업 랜덤 혹은 자연 랜덤 씬 부르기
            UI_category.SetActive(false);
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
