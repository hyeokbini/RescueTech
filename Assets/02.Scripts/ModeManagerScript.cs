using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManagerScript : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static ModeManagerScript Instance;

    // 연습 모드, 실전 모드 구분용 변수 ModeManager로 이동
    public bool isRealMode = true;

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // 씬 전환 시에도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
    }
}
