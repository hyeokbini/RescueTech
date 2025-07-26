using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeSceneModeInitializer : MonoBehaviour
{
    [SerializeField] private GameObject practiceManager;
    [SerializeField] private GameObject realManager;

    [SerializeField] private GameObject practiceObjects;
    [SerializeField] private GameObject realObjects;


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
                practiceObjects.SetActive(true);
                realManager.SetActive(false);
            }
            else
            {
                Debug.Log("실전 모드 실행");
                practiceManager.SetActive(false);
                realManager.SetActive(true);
                realObjects.SetActive(true);
            }
        }
    }
}

