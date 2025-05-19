using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;            //페이드 인/아웃 할 패널

    private Coroutine fadeCoroutine;    //페이드 코루틴 변수
    [SerializeField] 
    private string sceneName;           //씬 이름 변수(테스트 용)

    void Start()
    {
        FadeIn();   //씬 전환 시 페이드 인(테스트 용)
    }
    
    public void FadeIn(float speed = 0.05f)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeImage.gameObject.SetActive(true);
        fadeCoroutine = StartCoroutine(FadeInCoroutine(speed));
    }

    IEnumerator FadeInCoroutine(float speed)
    {
        //투명도가 0이 될때 까지 반복하는 코루틴
        Color color = fadeImage.color;
        color.a = 1f;
        while(color.a > 0f)
        {
            color.a -= speed;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);;
        }
        fadeImage.gameObject.SetActive(false);
        fadeCoroutine = null;
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
        //투명도가 1이 될때 까지 반복하는 코루틴
        Color color = fadeImage.color;
        color.a = 0f;
        while(color.a < 1f)
        {
            color.a += speed;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);;
        }
        fadeCoroutine = null;
        SceneManager.LoadScene(sceneName);      //페이드 아웃 후 씬 이동(테스트 용)
    }
}
