using UnityEngine;
using UnityEditor;

public class ShaderReplacer : EditorWindow
{
    [MenuItem("Tools/Replace All Shaders To Standard")]
    public static void ReplaceShaders()
    {
        int changedCount = 0;

        // 씬에 있는 모든 Renderer 검색
        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (mat != null && mat.shader.name != "Standard")
                {
                    mat.shader = Shader.Find("Standard");
                    changedCount++;
                }
            }
        }

        Debug.Log($"✅ 셰이더 변경 완료: {changedCount}개 머티리얼이 Standard 셰이더로 변경되었습니다.");
    }
}
