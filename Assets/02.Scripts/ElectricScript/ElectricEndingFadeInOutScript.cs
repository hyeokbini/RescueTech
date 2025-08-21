using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElectricEndingFadeInOutScript : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;            // 페이드 인/아웃 할 이미지
    private Coroutine fadeCoroutine;    // 코루틴 중복 방지용

    public IEnumerator FadeCoroutine(float speed = 0.01f)
    {
        // 중복 실행 방지
        if (fadeCoroutine != null)
            yield break;

        fadeCoroutine = StartCoroutine(FadeSequence(speed));
        yield return fadeCoroutine;
        fadeCoroutine = null;
    }

    private IEnumerator FadeSequence(float speed)
    {
        yield return StartCoroutine(FadeOutCoroutine(speed));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeInCoroutine(speed));
    }

    private IEnumerator FadeOutCoroutine(float speed)
    {
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (color.a < 1f)
        {
            color.a += speed;
            color.a = Mathf.Min(color.a, 1f);
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator FadeInCoroutine(float speed)
    {
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (color.a > 0f)
        {
            color.a -= speed;
            color.a = Mathf.Max(color.a, 0f);
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
