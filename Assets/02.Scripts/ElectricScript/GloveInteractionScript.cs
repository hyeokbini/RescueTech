using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ElectricPracticeManagerScript practiceGameManager;
    //[SerializeField]
    //private ElectricRealModeManagerScript realGameManager; // 추가
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private GameObject leftGlove;
    [SerializeField]
    private GameObject rightGlove;
    [SerializeField]
    private GameObject leftController;
    [SerializeField]
    private GameObject rightController;
    [SerializeField]
    private ElectricPlayerScript electricPlayer;
    [SerializeField]
    private int interactIndex = 0;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;

    public void WearGlove()
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

        if (leftGlove != null && leftController != null)
        {
            leftGlove.transform.SetParent(leftController.transform);
            leftGlove.transform.localPosition = Vector3.zero;
            leftGlove.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

            // 하위에서 "Model" 오브젝트 찾아서 비활성화
            Transform leftModel = leftController.transform.Find("Model");
            if (leftModel != null)
            {
                leftModel.gameObject.SetActive(false);
            }
        }

        if (rightGlove != null && rightController != null)
        {
            rightGlove.transform.SetParent(rightController.transform);
            rightGlove.transform.localPosition = Vector3.zero;
            rightGlove.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

            // 하위에서 "Model" 오브젝트 찾아서 비활성화
            Transform rightModel = rightController.transform.Find("Model");
            if (rightModel != null)
            {
                rightModel.gameObject.SetActive(false);
            }
        }


        electricPlayer.isWearGlove = true;
        hasInteracted = true;
        gameObject.SetActive(false);
    }
}
