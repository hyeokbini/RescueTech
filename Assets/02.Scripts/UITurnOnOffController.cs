using System.Collections;
using UnityEngine;

public class UITurnOnOffController : MonoBehaviour
{
    public bool autoHide = true;    // 자동 꺼짐 변수
    public float hideDelay = 3f;    // UI 숨기기 딜레이 변수

    private Coroutine hideCoroutine;

    // 딜레이 후 UI 숨기기 코루틴
    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        gameObject.SetActive(false);
        hideCoroutine = null;
    }

    // UI 켜는 함수
    public void ShowUI()
    {
        gameObject.SetActive(true);

        if (autoHide)
        {
            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(HideAfterDelay());
        }
    }

    // UI 끄는 함수
    public void HideUI()
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        gameObject.SetActive(false);
    }
}
