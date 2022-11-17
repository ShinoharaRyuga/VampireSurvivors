using UnityEngine;

/// <summary>Escapeキーでアプリを落とせるようにするクラス </summary>
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
