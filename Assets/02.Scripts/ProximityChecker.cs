using UnityEngine;
using System.Collections;

public class ProximityChecker : MonoBehaviour
{
    public GameObject highlights;
    private Transform player;
    public float triggerDistance = 1.5f;
    public float checkInterval = 0.2f; // 거리 체크 주기 (초 단위)

    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            StartCoroutine(CheckProximity());
        }
        else
        {
            Debug.LogWarning("Player 오브젝트를 찾을 수 없음");
        }
        StartCoroutine(CheckProximity());
    }

    IEnumerator CheckProximity()
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance < triggerDistance)
            {
                Debug.Log("가까이 옴");
                highlights.SetActive(true);
            }
            else{
                highlights.SetActive(false);
            }
            

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
