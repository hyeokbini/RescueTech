using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerAwakeScript : MonoBehaviour
{
    public float shakeMagnitude = 0.003f; // 흔들림 강도
    public float shakeSpeed = 100f;      // 흔들림 속도
    public float knockbackForce = 2f;    // 튕겨나는 힘

    private Vector3 originalPos;
    private Coroutine shockCoroutine;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        originalPos = transform.localPosition;
        StartShock();
    }

    // 감전 시작
    public void StartShock()
    {
        if (shockCoroutine != null)
        {
            StopShock();
        }

        shockCoroutine = StartCoroutine(ElectricShockEffect());
    }

    // 감전 종료 + 튕김
    public void StopShock()
    {
        if (shockCoroutine != null)
        {
            StopCoroutine(shockCoroutine);
            shockCoroutine = null;
        }

        // Rigidbody 힘으로 뒤로 튕기기
        rb.AddForce(-transform.forward * knockbackForce, ForceMode.Impulse);
    }

    IEnumerator ElectricShockEffect()
    {
        while (true) // 무한 흔들림
        {
            float x = Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude;
            float y = Mathf.Cos(Time.time * shakeSpeed * 1.2f) * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);
            yield return null;
        }
    }
}
