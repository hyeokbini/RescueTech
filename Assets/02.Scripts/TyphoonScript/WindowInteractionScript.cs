using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInteractionScript : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TyphoonPlayerScript player;
    [SerializeField]
    private WindowListManagerScript listManager;
    [SerializeField]
    private GameObject tape;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    public void ActiveTape()
    {
        if (player.isGrabTape)
        {
            if (hasInteracted) return;
            hasInteracted = true;
            listManager.IncreaseWindowCount();
            tape.SetActive(true);
            gameObject.SetActive(false); // 한번 켠 이후로는 UI 비활성화
        }
    }
}
