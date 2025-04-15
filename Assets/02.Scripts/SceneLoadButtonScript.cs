using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadButtonScript : MonoBehaviour
{
    [SerializeField] private string sceneName;

    // 연습 모드 상황에서 버튼을 클릭하면 해당되는 씬으로 연결
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // 실전 모드 상황에서 카테고리를 클릭하는 순간 각 상황에 맞는 랜덤 인덱스(0~2, 3~5) 중 하나로 넘어감
    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
