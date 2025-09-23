using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFSceneModeInitializer : MonoBehaviour
{
    [SerializeField] private GameObject practiceManager;
    [SerializeField] private GameObject realManager;

    // 상호 작용하는 오브젝트를 구분한다. 
    [SerializeField] private GameObject directionObjects;


    private bool isRealMode;

    void Start()
    {
        // Null 에러 처리
        if (ModeManagerScript.Instance == null)
        {
            Debug.LogError("ModeManagerScript.Instance 할당 안됨");
        }
        else
        {
            isRealMode = ModeManagerScript.Instance.isRealMode;

            if (!isRealMode)
            {
                Debug.Log("연습 모드 실행");
                practiceManager.SetActive(true);
                directionObjects.SetActive(true);
                realManager.SetActive(false);
            }
            else
            {
                Debug.Log("실전 모드 실행");
                practiceManager.SetActive(false);
                directionObjects.SetActive(false);
                realManager.SetActive(true);
            }
        }
    }
}

