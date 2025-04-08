using UnityEngine;

public class PracticeModeGameManagerScript : MonoBehaviour
{
    [SerializeField]
    private int stageStepCount; // 스테이지에서 진행해야 할 총 단계
    private int currentStepCount = 0; // 현재 단계
    [SerializeField]
    private GameObject clearUIPanel; // ui 오브젝트 연결
    
    // 각 오브젝트에 gamemanager를 참조해서 이 함수 실행
    public void IncreaseStageStep()
    {
        currentStepCount++;
        // 스테이지 클리어 로직 체크
        if(currentStepCount == stageStepCount)
        {
            CheckStageClear();
        }
    }

    // 스테이지 클리어 로직
    private void CheckStageClear()
    {
        if (clearUIPanel != null)
        {
            clearUIPanel.SetActive(true);
        }
        else
        {
            Debug.Log("UI 할당되지 않음");
        }
    }
}
