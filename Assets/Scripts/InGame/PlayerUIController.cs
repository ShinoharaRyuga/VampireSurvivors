using UnityEngine;

/// <summary>プレイヤーUIを制御するクラス</summary>
public class PlayerUIController : MonoBehaviour
{
    RectTransform _playerUI => GetComponent<RectTransform>();

    void Update()
    {
        // 自身の向きをカメラに向ける
        _playerUI.LookAt(Camera.main.transform);
    }
}
