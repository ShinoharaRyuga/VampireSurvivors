using TMPro;
using UnityEngine;

/// <summary>���������I������N���X </summary>
public class SelectFirstWeapon : MonoBehaviour
{
    [SerializeField, Tooltip("0=�i�C�t 1=���@�̏�@2=���̏�@3=�����@4=���@5=�j���j�N�@6=�n�j��")] Weapons _firstWeapon = Weapons.Knife;
    [SerializeField, Tooltip("���������\������e�L�X�g")] TextMeshProUGUI _firstWeaponText = default;
    [SerializeField, Tooltip("�g�p����̖��O")] string _weaponName = "";

    /// <summary>
    /// �����I������
    /// �{�^���������ꂽ��Ăяo�����
    /// </summary>
    public void SelectWeapon()
    {
        GameManager.Instance.FirstWeapon = _firstWeapon;
        _firstWeaponText.text = $"�g�p����:{_weaponName}";
    }
}
