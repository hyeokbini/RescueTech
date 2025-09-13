using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasFadeController : MonoBehaviour
{
    [SerializeField] 
    private Image fadeImage;
    [SerializeField]
    private float fadeSpeed = 0.05f;
    private Coroutine fadeCoroutine;
    private bool isFading = false;
    public bool IsFading => isFading;


    public void FadeIn()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeImage.gameObject.SetActive(true);
        fadeCoroutine = StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        isFading = true;           
        Color color = fadeImage.color;
        color.a = 1f;
        while (color.a > 0f)
        {
            color.a -= fadeSpeed;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        fadeImage.gameObject.SetActive(false);
        fadeCoroutine = null;
        isFading = false;           
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeImage.gameObject.SetActive(true);
        fadeCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        isFading = true;             
        Color color = fadeImage.color;
        color.a = 0f;
        while (color.a < 1f)
        {
            color.a += fadeSpeed;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        fadeCoroutine = null;
        isFading = false;            
    }
}
