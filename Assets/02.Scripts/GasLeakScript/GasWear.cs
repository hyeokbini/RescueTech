using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GasWearType
{
    Mask,
    Glove
}

public class GasWear : MonoBehaviour
{
    [SerializeField]
    private GasPracticeModeManager gasPracticeModeManager;
    [SerializeField]
    private TextUIManagerScript textUIManager;
    private bool maskWeared = false;
    private bool gloveWeared = false;

    public void Check(GasWearType type)
    {
        if (type == GasWearType.Mask)
            maskWeared = true;
        if (type == GasWearType.Glove)
            gloveWeared = true;
        if (maskWeared && gloveWeared)
        {
            if (ModeManagerScript.Instance.isRealMode)
            {

            }
            else
            {
                textUIManager.IncreaseIndex();
                textUIManager.ActivateUIWithText();
                gasPracticeModeManager.IncreaseStageStep();
            }
        }
    }
}
