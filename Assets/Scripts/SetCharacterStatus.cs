using TMPro;
using UnityEngine;

public class SetCharacterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�̗�, ��, �A�[�}�[, �ړ����x, �З�, �G���A, ���x, ��������, ��,�@�N�[���_�E��, �^�C,�@����, ���~, ��, ���� ��������̓Y����")]
    float[] _characterStatus = new float[16];
    [SerializeField, Tooltip("���������\������e�L�X�g")] TextMeshProUGUI _firstWeaponText = default;
    [SerializeField, Tooltip("�g�p����̖��O")] string _weaponName = "";

    public void SetStatus()
    {
        GameManager.Instance.SelectedCharacterStatus = _characterStatus;
        _firstWeaponText.text = $"�g�p����:{_weaponName}";
    }
}
