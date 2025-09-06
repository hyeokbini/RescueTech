using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyCallInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ElectricPracticeManagerScript practiceGameManager;
    //[SerializeField]
    //private ElectricRealModeManagerScript realGameManager; // 추가
    [SerializeField]
    private TextUIManagerScript textManager;

    [SerializeField]
    private int interactIndex = 2;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;


    public void WorkerInteraction()
    {
        if (hasInteracted) return;

        if (!ModeManagerScript.Instance.isRealMode)
        {
            textManager.IncreaseIndex();
            textManager.ActivateUIWithText();
            practiceGameManager.IncreaseStageStep();
        }
        else
        {
            //realGameManager.AddScore(100); // 실전 모드 점수 부여
            //realGameManager.getCompletedActionList[1] = true;
        }
        hasInteracted = true;
        gameObject.SetActive(false);
    }
}