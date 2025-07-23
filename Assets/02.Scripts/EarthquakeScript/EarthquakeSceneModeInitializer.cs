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

    private void Awake()
    {
        isRealMode = ModeManagerScript.Instance.isRealMode;
        
        if (!isRealMode)
        {
            practiceManager.SetActive(true);
            practiceObjects.SetActive(true);
            realManager.SetActive(false);
        }
        else
        {
            practiceManager.SetActive(false);
            realManager.SetActive(true);
            realObjects.SetActive(true);
        }
    }
}

