using UnityEngine;

/// <summary>Escape�L�[�ŃA�v���𗎂Ƃ���悤�ɂ���N���X </summary>
public class CloseApp : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
