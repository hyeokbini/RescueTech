using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskGlove : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int interactIndex = 3;
    public int InteractIndex => interactIndex;
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    [SerializeField]
    private GasWearType gasWearType;
    [SerializeField]
    GasWear gasWear;
    [SerializeField]
    private GameObject leftGlove;
    [SerializeField]
    private GameObject rightGlove;
    [SerializeField]
    private GameObject leftController;
    [SerializeField]
    private GameObject rightController;
    [SerializeField]
    private GameObject gasMask;
    public void Wear()
    {
        if (hasInteracted) return;
        hasInteracted = true;
        if (gasWearType == GasWearType.Glove)
        {
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
        }
        else if (gasWearType == GasWearType.Mask)
        {
            if (gasMask) gasMask.SetActive(false);
        }
        gasWear.Check(gasWearType);
    }
}
