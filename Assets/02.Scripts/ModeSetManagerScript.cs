using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSetManagerScript : MonoBehaviour
{
    // 연습모드, 실전모드 오브젝트 그룹의 부모 오브젝트 참조
    [SerializeField] private GameObject realModeObjGroup;
    [SerializeField] private GameObject practiceModeObjGroup;
    void Awake()
    {
        if(ModeManagerScript.Instance.isRealMode)
        {
            realModeObjGroup.SetActive(true);
            practiceModeObjGroup.SetActive(false);
        }
        else
        {
            realModeObjGroup.SetActive(false);
            practiceModeObjGroup.SetActive(true);
        }
    }
}
