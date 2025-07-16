using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject tvScreen;
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private int Interactidx = 0;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    public void TurnOnTvScreen()
    {
        if (hasInteracted && Interactidx != gameManager.currentStepCount) return;
        textManager.IncreaseIndex();
        textManager.ActivateUIWithText();
        hasInteracted = true;
        gameManager.IncreaseStageStep();
        tvScreen.SetActive(true);
        gameObject.SetActive(false); // 한번 켠 이후로는 UI 비활성화
    }
}
