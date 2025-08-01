using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSwitchInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TyphoonRealModeManagerScript realGameManager; // 추가
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private GameObject offSwitch;
    [SerializeField]
    private GameObject onSwitch;
    [SerializeField]
    private int interactIndex = 2;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void TurnOnSwitch()
    {
        if (hasInteracted) return;

        if (!ModeManagerScript.Instance.isRealMode)
        {
            textManager.IncreaseIndex();
            textManager.ActivateUIWithText();
            gameManager.IncreaseStageStep();
        }
        else
        {
            realGameManager.AddScore(100); // 실전 모드 점수 부여
        }

        hasInteracted = true;
        offSwitch.gameObject.SetActive(false);
        onSwitch.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}