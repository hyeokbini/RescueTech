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

    public GameObject[] objectList;
    private int currentUI;


    void Start()
    {
        // 게임 오브젝트 배열 생성
        objectList = new GameObject[8]; 

        objectList[0] = UI_intro;
        objectList[1] = UI_tutorial;
        objectList[2] = UI_mode;
        objectList[3] = UI_category;
        objectList[4] = UI_naturalSituation;
        objectList[5] = UI_industrySituation;
        objectList[6] = UI_naturalSelectButton;
        objectList[7] = UI_industrySelectButton;
        
        StartCoroutine(ShowIntro(2f));
    }

    IEnumerator ShowIntro(float delay)
    {
        currentUI = 0;
        objectList[currentUI].SetActive(true);
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
        currentUI = 1;
        objectList[currentUI - 1].SetActive(false);
        objectList[currentUI].SetActive(true);
    }

    // 모드 선택창을 보여주는 메서드
    public void ShowModeBtn()
    {
        Debug.Log("모드 선택 체크");
        currentUI = 2;
        objectList[currentUI - 1].SetActive(false);
        objectList[currentUI].SetActive(true);
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
        currentUI = 3;
        objectList[currentUI - 1].SetActive(false);
        objectList[currentUI].SetActive(true);
    }

    // 카테고리 확인 메서드
    public void SetIndustry()
    {
        // 실전 모드 선택시, 이 버튼을 누르는 시점에서 바로 씬 전환되어야 함
        if(ModeManagerScript.Instance.isRealMode)
        {
            int randSceneIdx = Random.Range(4, 7);
            objectList[7].GetComponent<SceneLoadButtonScript>().LoadScene(randSceneIdx);
        }
        isNatural = false;
        Debug.Log("산업 재난 선택");
    }

    public void SetNatural()
    {
        // 실전 모드 선택시, 이 버튼을 누르는 시점에서 바로 씬 전환되어야 함
        if(ModeManagerScript.Instance.isRealMode)
        {
            int randSceneIdx = Random.Range(1, 4);
            objectList[6].GetComponent<SceneLoadButtonScript>().LoadScene(randSceneIdx);
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
            Debug.Log("랜덤 씬 변환");
        }
        else
        {
            if (isNatural)
            {
                Debug.Log("자연 재난 상황 선택");
                currentUI = 4;
                objectList[currentUI - 1].SetActive(false);
                objectList[currentUI].SetActive(true);
            }
            else 
            {
                Debug.Log("산업 재난 상황 선택");
                currentUI = 5;
                objectList[currentUI - 2].SetActive(false);
                objectList[currentUI].SetActive(true);
            }
        }
        

    }
    public void BackBtn(){
        // 산업재해 선택은 두 단계 전으로 올라감
        if (currentUI == 5){
            // 현재 ui 비활성화
            objectList[5].SetActive(false);
            // 그 전 단계 ui 활성화
            objectList[3].SetActive(true);
            // 현재 ui 설정
            currentUI = 3;
        }
        else {
            // 현재 ui 비활성화
            objectList[currentUI].SetActive(false);
            // 그 전 단계 ui 활성화
            objectList[currentUI - 1].SetActive(true);
            // 현재 ui 설정
            currentUI -= 1;
        }
    }
}

