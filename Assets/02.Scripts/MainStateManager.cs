using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainStateManager : MonoBehaviour
{
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
    [SerializeField]
    private GameObject UI_naturalSelectButton;
    [SerializeField]
    private GameObject UI_industrySelectButton;

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
    }

    // 모드 확인 메서드
    public void SetPracticeMode()
    {
        ModeManagerScript.Instance.isRealMode = false;
        Debug.Log("연습 모드 선택");
    }

    public void SetRealMode()
    {
        ModeManagerScript.Instance.isRealMode = true;
        Debug.Log("실전 모드 선택");
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
        // 실전 모드 선택시, 이 버튼을 누르는 시점에서 바로 씬 전환되어야 함
        if(ModeManagerScript.Instance.isRealMode)
        {
            int randSceneIdx = Random.Range(3, 6);
            UI_industrySelectButton.GetComponent<SceneLoadButtonScript>().LoadScene(randSceneIdx);
        }
        isNatural = false;
        Debug.Log("산업 재난 선택");
    }

    public void SetNatural()
    {
        // 실전 모드 선택시, 이 버튼을 누르는 시점에서 바로 씬 전환되어야 함
        if(ModeManagerScript.Instance.isRealMode)
        {
            int randSceneIdx = /*Random.Range(0, 3);*/ 0;
            UI_naturalSelectButton.GetComponent<SceneLoadButtonScript>().LoadScene(randSceneIdx);
        }
        isNatural = true;
        Debug.Log("자연 재난 선택");
    }

    //각 재난은 Situation으로 명명
    // 재난 상황 선택창을 보여주는 메서드
    public void ShowSituationBtn()
    {
        if (ModeManagerScript.Instance.isRealMode)
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
