using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TyphoonRealModeManagerScript realGameManager; // 추가
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void GrabPhone()
    {
        if (hasInteracted) return;

        if (!ModeManagerScript.Instance.isRealMode)
        {
            textManager.IncreaseIndex();
            gameManager.IncreaseStageStep();
        }
        else
        {
            realGameManager.AddScore(50); // 실전 모드 점수 부여
            realGameManager.getCompletedActionList[2] = true;
        }
        hasInteracted = true;
    }
}

