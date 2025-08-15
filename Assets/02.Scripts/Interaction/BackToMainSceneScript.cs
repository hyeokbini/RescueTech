using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 필수
using HTC.UnityPlugin.Vive;

public class BackToMainSceneScript : MonoBehaviour
{
    public HandRole hand = HandRole.RightHand;

    void Update()
    {
        // Grip 버튼 눌렀을 때
        if (ViveInput.GetPressDown(hand, ControllerButton.Grip))
        {
            // 현재 씬 이름이 Mainscene이 아니라면
            if (SceneManager.GetActiveScene().name != "Mainscene")
            {
                SceneManager.LoadScene("Mainscene");
            }
        }
    }
}
