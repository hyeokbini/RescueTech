using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeManager : MonoBehaviour
{
    // 지진 사이클 관련 
    private bool isShaking = false;
    private float shakingInterval = 5f;
    private float idleInterval = 3f;

    // 지진 관련
    [SerializeField] private float shakeDuration = 5f;
    [SerializeField] private float shakeAmount = 0.8f;      // 지진의 진폭
    [SerializeField] private float speed = 2f;         // 지진의 속도
    private float returnDuration = 0.5f;

    private Vector3 originalPos;
    private Transform originalTransform;

    void Start()
    {
        originalTransform = transform;
        originalPos = originalTransform.localPosition;

        StartCoroutine(ShakingCycleCoroutine());
    }

    IEnumerator ShakingCycleCoroutine()
    {
        while (true)
        {
            if (isShaking)
            {
                Debug.Log("흔들림 시작");
                StartCoroutine(ShakingCoroutine());
                yield return new WaitForSeconds(shakingInterval);
                isShaking = false;
            }
            else
            {
                Debug.Log("안 흔들리는 중");
                yield return new WaitForSeconds(idleInterval);
                isShaking = true;
            }
        }
    }

    IEnumerator ShakingCoroutine()
    {
        float currentTime = 0f;

        while (currentTime < shakingInterval)
        {
            currentTime += Time.deltaTime;
            {
                // PerlinNoise로 -1 ~ 1 사이에  랜덤값 생성
                float offsetX = (Mathf.PerlinNoise(Time.time * speed, 0f) - 0.5f) * 2f;
                float offsetZ = (Mathf.PerlinNoise(0f, Time.time * speed) - 0.5f) * 2f;
                Vector3 targetPos = originalPos + new Vector3(offsetX, 0f, offsetZ) * shakeAmount;

                originalTransform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);
            }

            yield return null;
        }

        // 원위치로 돌아올 때도 부드럽게
        float t = 0f;
        Vector3 startPos = transform.localPosition;

        while (t < returnDuration)
        {
            Debug.Log("원위치");
            t += Time.deltaTime;
            float ratio = t / returnDuration;
            transform.localPosition = Vector3.Lerp(startPos, originalPos, ratio);
            yield return null;
        }

        transform.localPosition = originalPos;
    }

}


