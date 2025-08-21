using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireModePhone : MonoBehaviour, IInteractable
{
    [SerializeField]
    private FirePracticeModeManager firePracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private GameObject phoneUI;
    [SerializeField]
    private int interactIndex = 1;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    [SerializeField]
    private GameObject btnClickUI;
    public List<string> btnClickText;
    private bool isCoroutineRunning = false;

    void Start()
    {
        phoneUI.SetActive(false);
        btnClickUI.SetActive(false);

    }

    public void TurnOnPhone()
    {
        if (hasInteracted) return;
        phoneUI.SetActive(true);
    }

    public void CallTo119(int index)
    {
        if (hasInteracted)
        {
            OnBtnClick(3);
            return;
        }
        hasInteracted = true;
        OnBtnClick(index);
        if(ModeManagerScript.Instance.isRealMode)
        {
            FireScoreManager.Instance.CompleteAction(FireAction.Phone_119);
        }
        else
        {
            textUIManager.IncreaseIndex();
            textUIManager.ActivateUIWithText();
            firePracticeModeManager.IncreaseStageStep();
        }
    }

    public void UseAnotherApp(int index)
    {
        if (hasInteracted)
        {
            OnBtnClick(3);
            return;
        }
        if(ModeManagerScript.Instance.isRealMode)
        {
            FireScoreManager.Instance.CompleteAction(FireAction.Phone_Another);
            hasInteracted = true;
            OnBtnClick(index);
        }
    }

    public void OnBtnClick(int index)
    {
        if (isCoroutineRunning) return;
        if (btnClickText == null || btnClickUI == null) return;
        if (index < 0 || index >= btnClickText.Count) return;
        TextMeshProUGUI textComponent = btnClickUI.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = btnClickText[index];
        StartCoroutine(UICoroutine());
    }

    private IEnumerator UICoroutine()
    {
        isCoroutineRunning = true;
        btnClickUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        btnClickUI.SetActive(false);
        isCoroutineRunning = false;
    }
}
