using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>����̃V�[���ɑJ�ڂ���</summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>�V�[���J�ڂ��s��</summary>
    /// <param name="sceneName">�J�ڐ�̃V�[����</param>
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
