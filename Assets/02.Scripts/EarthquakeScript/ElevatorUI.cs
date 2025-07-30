using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUI : MonoBehaviour
{
    private Coroutine activePannelCoroutine;

    public void showUI(){
        gameObject.SetActive(true); // 코루틴 시작 전에 반드시 활성화

        if (activePannelCoroutine != null)
        {
            StopCoroutine(activePannelCoroutine);
        }
        activePannelCoroutine = StartCoroutine(ActivePannelCoroutine());
    }

    // 안내 문구 표시 코루틴
    IEnumerator ActivePannelCoroutine()
    {
        // 3초 대기
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        activePannelCoroutine = null;
    }
}
