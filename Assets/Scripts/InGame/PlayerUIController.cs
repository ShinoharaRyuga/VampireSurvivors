using UnityEngine;

/// <summary>�v���C���[UI�𐧌䂷��N���X</summary>
public class PlayerUIController : MonoBehaviour
{
    RectTransform _playerUI => GetComponent<RectTransform>();

    void Update()
    {
        // ���g�̌������J�����Ɍ�����
        _playerUI.LookAt(Camera.main.transform);
    }
}
