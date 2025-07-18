using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EarthquakeFadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    private Coroutine fadeCoroutine;
    private bool isFading = false;
    public bool IsFading => isFading;


    public void FadeIn(float speed = 0.05f)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeImage.gameObject.SetActive(true);
        fadeCoroutine = StartCoroutine(FadeInCoroutine(speed));
    }

    IEnumerator FadeInCoroutine(float speed)
    {
        isFading = true;           
        Color color = fadeImage.color;
        color.a = 1f;
        while (color.a > 0f)
        {
            color.a -= speed;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        fadeImage.gameObject.SetActive(false);
        fadeCoroutine = null;
        isFading = false;           
    }

    public void FadeOut(float speed = 0.05f)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeImage.gameObject.SetActive(true);
        fadeCoroutine = StartCoroutine(FadeOutCoroutine(speed));
    }

    IEnumerator FadeOutCoroutine(float speed)
    {
        isFading = true;             
        Color color = fadeImage.color;
        color.a = 0f;
        while (color.a < 1f)
        {
            color.a += speed;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        fadeCoroutine = null;
        isFading = false;            
    }
}

