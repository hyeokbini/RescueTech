using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInteractionScript : MonoBehaviour
{
    [SerializeField]
    private GameObject tvScreen;

    public void TurnOnTvScreen()
    {
        tvScreen.SetActive(true);
        gameObject.SetActive(false); // 한번 켠 이후로는 UI 비활성화
    }
}
