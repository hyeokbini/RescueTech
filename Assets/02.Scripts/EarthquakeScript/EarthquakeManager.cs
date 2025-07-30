using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeManager : MonoBehaviour
{
    // 지진 사이클 관련 
    [SerializeField] private bool isShaking = false;
    [SerializeField] private float shakingInterval = 5f;
    [SerializeField] private float idleInterval = 3f;

    // 지진 관련
    [SerializeField] private float shakeAmount = 0.8f;      // 지진의 진폭
    [SerializeField] private float updateInterval = 0.1f;     // 진동의 속도
    [SerializeField] private float speed = 2f;         // 지진의 속도
    [SerializeField] private float rotationAmount = 5f;

    
    [SerializeField] private EarthquakePracticeModeGameManagerScript earthquakeStageManager;
    [SerializeField] private EarthquakePlayer earthquakePlayer;
    private float returnDuration = 0.5f;

    private Vector3 originalPos;
    private Quaternion originalRot;
    private Transform originalTransform;

    void Start()
    {
        originalTransform = transform;
        originalPos = originalTransform.localPosition;
        originalRot = originalTransform.localRotation;

        StartCoroutine(ShakingCycleCoroutine());
    }

    IEnumerator ShakingCycleCoroutine()
    {   
        // 지진 반복
        while (true)
        {
            // 지진 
            if (isShaking)
            {
                StartCoroutine(ShakingCoroutine());
                yield return new WaitForSeconds(shakingInterval);
                isShaking = false;
            }
            // 유휴 
            else
            {
                yield return new WaitForSeconds(idleInterval);
                isShaking = true;
            }
            
            // 연습모드에서 6단계에 왔거나 earthquakePlayer가 동작을 수행했다면(hasInteracted가 true라면)
            if (earthquakeStageManager.CurrentStepCount == 6 || earthquakePlayer.hasInteracted) {
                Debug.Log("지진 정지");
                isShaking = false;
                break;
            }
        }
    }

    IEnumerator ShakingCoroutine()
    {
        float currentTime = 0f;
        float updateTime = 0f;
        Vector3 targetPos = originalPos;
        Quaternion targetRot = originalRot;

        while (currentTime < shakingInterval)
        {
            currentTime += Time.deltaTime;
            updateTime += Time.deltaTime;

            if (updateTime >= updateInterval)
            {
                updateTime = 0f;
                float offsetX = Random.Range(-shakeAmount, shakeAmount);
                float offsetY = Random.Range(-shakeAmount, shakeAmount);
                float offsetZ = Random.Range(-shakeAmount, shakeAmount);
                targetPos = originalPos + new Vector3(offsetX, offsetY, offsetZ);

                // 회전 흔들림
                float rotX = Random.Range(-rotationAmount, rotationAmount);
                float rotY = Random.Range(-rotationAmount, rotationAmount);
                float rotZ = Random.Range(-rotationAmount, rotationAmount);
                targetRot = Quaternion.Euler(rotX, rotY, rotZ) * originalRot;
            }
            
            originalTransform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);
            originalTransform.localRotation = Quaternion.Slerp(originalTransform.localRotation, targetRot, speed * Time.deltaTime);


            yield return null;
        }

        // 원위치로 돌아올 때도 부드럽게
        float t = 0f;
        Vector3 startPos = transform.localPosition;
        Quaternion startRot = originalTransform.localRotation;

        while (t < returnDuration)
        {
            t += Time.deltaTime;
            float ratio = t / returnDuration;
            transform.localPosition = Vector3.Lerp(startPos, originalPos, ratio);
            originalTransform.localRotation = Quaternion.Slerp(startRot, originalRot, ratio);
            yield return null;
        }

        originalTransform.localPosition = originalPos;
        originalTransform.localRotation = originalRot;
    }

}


