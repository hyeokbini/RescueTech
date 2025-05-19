using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUIManagerScript : MonoBehaviour
{
    [SerializeField, TextArea]
    private List<string> textUIList = new List<string>();
    [SerializeField]
    private GameObject textPannel;
    [SerializeField]
    private GameObject textObject;
    private TextMeshProUGUI textComponent;
    private int currentIndex = 0;
    private Coroutine activePannelCoroutine;

    public void ActivateUIWithText()
    {
        if (activePannelCoroutine != null)
        {
            StopCoroutine(activePannelCoroutine);
        }
        activePannelCoroutine = StartCoroutine(ActivePannelCoroutine());
    }

    public void IncreaseIndex()
    {
        if (currentIndex + 1 < textUIList.Count)
        {
            currentIndex++;
        }
    }

    IEnumerator ActivePannelCoroutine()
    {
        textPannel.SetActive(true);
        textComponent = textObject.GetComponent<TextMeshProUGUI>();
        if (textComponent == null)
        {
            Debug.Log("할당안됨");
            yield return null;
        }
        textComponent.text = textUIList[currentIndex];
        // 10초 대기
        yield return new WaitForSeconds(10f);
        textPannel.SetActive(false);
        activePannelCoroutine = null;
    }
}
