using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ElectricPracticeManagerScript practiceGameManager;
    //[SerializeField]
    //private ElectricRealModeManagerScript realGameManager; // 추가
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private GameObject offSwitch;
    [SerializeField]
    private GameObject onSwitch;
    [SerializeField]
    private int interactIndex = 0;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    [SerializeField]
    private WorkerAwakeScript workerScript;
    [SerializeField]
    private GameObject spark;

    public void TurnOffSwitch()
    {
        if (hasInteracted) return;

        workerScript.StopShock();
        spark.SetActive(false);

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
        offSwitch.gameObject.SetActive(false);
        onSwitch.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}