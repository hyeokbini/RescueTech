using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowListManagerScript : MonoBehaviour
{
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TyphoonRealModeManagerScript realGameManager;  // 추가

    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private int allWindowCount;
    public int currentWindowCount = 0;

    public void IncreaseWindowCount()
    {
        currentWindowCount++;

        // 실전 모드일 경우 창문 하나당 점수 10점 추가
        if (ModeManagerScript.Instance.isRealMode)
        {
            realGameManager.AddScore(10);
        }

        if (allWindowCount == currentWindowCount)
        {
            if (!ModeManagerScript.Instance.isRealMode)
            {
                textManager.IncreaseIndex();
                textManager.ActivateUIWithText();
                gameManager.IncreaseStageStep();
            }
            else
            {
                realGameManager.AddScore(30); // 실전 모드: 추가점 30점
            }
        }
    }

}
