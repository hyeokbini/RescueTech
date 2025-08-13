using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElevator : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int interactIndex = -1;
    public int InteractIndex => interactIndex;
    public bool hasInteracted = false;
    public bool HasInteracted => hasInteracted;
    [SerializeField] 
    private Transform player;
    [SerializeField] 
    private Transform respawnPoint;
    [SerializeField]
    private FireFadeController fireFadeController;

    public void UseElevator()
    {
        if (hasInteracted || !ModeManagerScript.Instance.isRealMode) return;
        StartCoroutine(ElevatorCoroutine());
    }

    private IEnumerator ElevatorCoroutine()
    {
        fireFadeController.FadeOut();
        while (fireFadeController.IsFading) yield return null;
        FireScoreManager.Instance.CompleteAction(FireAction.Elevator);
        hasInteracted = true;
        if (player != null && respawnPoint != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                player.position = respawnPoint.position;
                controller.enabled = true;
                Debug.Log($"[리스폰 완료] Player 위치: {player.position}");
            }
            else
                Debug.LogError("카메라 또는 캐릭터 컨트롤러를 찾지 못함");
        }
        fireFadeController.FadeIn();
        while (fireFadeController.IsFading) yield return null;
    }
}
