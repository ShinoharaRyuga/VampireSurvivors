using TMPro;
using UnityEngine;

public class SetCharacterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�̗́@�񕜁@�ړ����x�@���΁@�ʁ@��������")] float[] _characterStatus = new float[6];
    [SerializeField, Tooltip("���������\������e�L�X�g")] TextMeshProUGUI _firstWeaponText = default;
    [SerializeField, Tooltip("�g�p����̖��O")] string _weaponName = "";

    public void SetStatus()
    {
        GameManager.Instance.SelectedCharacterStatus = _characterStatus;
        _firstWeaponText.text = $"�g�p����:{_weaponName}";
    }
}
