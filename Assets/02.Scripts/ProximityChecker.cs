using UnityEngine;
using System.Collections;

public class ProximityChecker : MonoBehaviour
{
    public GameObject highlights;
    public Transform player;
    public float triggerDistance = 3f;
    public float checkInterval = 0.2f; // 거리 체크 주기 (초 단위)

    void Start()
    {
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
