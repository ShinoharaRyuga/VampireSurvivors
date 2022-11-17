using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>特定のシーンに遷移する</summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>シーン遷移を行う</summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
