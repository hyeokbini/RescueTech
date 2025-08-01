using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject tvScreen;
    [SerializeField]
    private TyphoonPracticeModeManagerScript pracitceGameManager;
    [SerializeField]
    private TyphoonRealModeManagerScript realGameManager;
    [SerializeField]
    private TextUIManagerScript practicetextManager;
    [SerializeField]
    private TextUIManagerScript realtextManager;
    [SerializeField]
    private int interactIndex = 0;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    public void TurnOnTvScreen()
    {
        if (hasInteracted) return;
        if (!ModeManagerScript.Instance.isRealMode)
        {
            practicetextManager.IncreaseIndex();
            practicetextManager.ActivateUIWithText();
            pracitceGameManager.IncreaseStageStep();
        }
        else
        {
            realGameManager.StartStage();
            realtextManager.IncreaseIndex();
            realtextManager.ActivateUIWithText();
            realGameManager.IncreaseStep(); // 실전 모드에서는 실전용 호출
        }
        hasInteracted = true;
        tvScreen.SetActive(true);
        gameObject.SetActive(false); // 한번 켠 이후로는 UI 비활성화
    }
}
