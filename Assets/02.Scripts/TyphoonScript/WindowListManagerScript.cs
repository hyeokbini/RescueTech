using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowListManagerScript : MonoBehaviour
{
    [SerializeField]
    private TyphoonPracticeModeManagerScript gameManager;
    [SerializeField]
    private TextUIManagerScript textManager;
    [SerializeField]
    private int allWindowCount;
    public int currentWindowCount = 0;

    public void IncreaseWindowCount()
    {
        currentWindowCount++;
        if (allWindowCount == currentWindowCount)
        {
            textManager.IncreaseIndex();
            textManager.ActivateUIWithText();
            gameManager.IncreaseStageStep();
        }
    }
}
