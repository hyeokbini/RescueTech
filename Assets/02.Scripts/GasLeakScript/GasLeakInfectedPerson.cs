using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GasLeakInfectedPerson : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    [SerializeField]
    private int interactIndex = 5;
    [SerializeField]
    private GameObject original;
    [SerializeField]
    private GameObject infectedPerson;
    [SerializeField]
    private GasFadeController gasFadeController;
    [SerializeField]
    private Transform playerRespawnPoint;
    [SerializeField] 
    private Transform player;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void FirstAid()
    {
        if (hasInteracted) return;
        gasFadeController.FadeOut();
        original.gameObject.SetActive(false);
        infectedPerson.gameObject.SetActive(true);
        player.position = playerRespawnPoint.position;
        gasFadeController.FadeIn();
        if (ModeManagerScript.Instance.isRealMode)
        {
            GasLeakScoreManager.Instance.CompleteAction(GasAction.Person);
            hasInteracted = true;
        }
        else
        {
            textUIManager.IncreaseIndex();
            textUIManager.ActivateUIWithText();
            hasInteracted = true;
            gasPracticeModeManager.IncreaseStageStep();
        }
    }
}
