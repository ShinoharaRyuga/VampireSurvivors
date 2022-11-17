using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>����֘A�̏������Ǘ�����N���X </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField, Tooltip("0=�i�C�t 1=���@�̏�@2=���̏�@3=�����@4=���@5=�j���j�N�@6=�n�j��")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("0=�ő�̗� 1=������ 2=���� 3=�ړ����x 4=��")] EffectWeaponBase[] _effectWeapon = default;
    [SerializeField, Tooltip("����I�����鎞�Ɏg��UI")] GameObject _weaponSelectCanvas = default;
    [SerializeField, Tooltip("����I��������ׂ̃{�^��")] Button[] _weaponSelectButtons = new Button[4];

    /// <summary>�v���C���[���V����������l������</summary>
    /// <param name="index"></param>
    /// <param name="type"></param>
    public void GetWeapon(int index, WeaponType type)
    {
        if (type == WeaponType.Weapon)
        {
            if (index == 5)�@ //�j���j�N�̍U�����J�n����
            {
                var go = Instantiate(_weapons[5], GameManager.Instance.Player.transform);
                StartCoroutine(go.Generator());
                return;
            }

            StartCoroutine(_weapons[index].Generator());
        }
        else
        {
            _effectWeapon[index].Effect();
        }
    }

    /// <summary>�S����̐��\������������ </summary>
    public void ResetWeapons()
    {
        foreach (var weapon in _weapons)
        {
            weapon.GeneratorNumber = 1;
            weapon.ResetStatus();
        }
    }

    /// <summary>�v���C���[�����x���A�b�v�������Ă΂�ăv���C���[���I�����镐���I�� </summary>
    public void SetSelectWeapons()
    {
        var selectedWeapons = new List<WeaponSelectTable>();
        var totalProb = GameData.WeaponSelectTables.Sum(x => x.Probability);
        var rand = UnityEngine.Random.Range(0, totalProb);

        while (selectedWeapons.Count < _weaponSelectButtons.Length)�@  //�I���\�ȕ����I�o����
        {
            foreach (var data in GameData.WeaponSelectTables)
            {
                if (rand < data.Probability && !selectedWeapons.Contains(data) && data.Level < _weapons[data.Id].MaxLevel)
                {
                    selectedWeapons.Add(data);
                    rand = UnityEngine.Random.Range(0, totalProb);
                    break;
                }
                rand -= data.Probability;
            }
        }

        for (var i = 0; i < _weaponSelectButtons.Length; i++)�@//�{�^���Ɋ֐���ǉ�����
        {
            var weapon = selectedWeapons[i];
            _weaponSelectButtons[i].onClick.AddListener(() => SelectWeapon(weapon));
            _weaponSelectButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedWeapons[i].Name;
        }
        _weaponSelectCanvas.SetActive(true);
    }

    /// <summary>����I���{�^����OnClick�ɒǉ������ </summary>
    private void SelectWeapon(WeaponSelectTable weapon)
    {
        weapon.Level++;
        if (weapon.Level == 1)  //�V����������l��
        {
            GetWeapon(weapon.Id, weapon.Type);
        }
        else�@�@�@�@�@�@�@�@�@�@//����̃��x�����グ��
        {
            if (weapon.Type == WeaponType.Weapon)�@//�U������
            {
                _weapons[weapon.Id].LevelUp(weapon.Level);
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@//���ʕ���
            {
                   _effectWeapon[weapon.Id].LevelUp();
            }
        }

        _weaponSelectCanvas.SetActive(false);
        GameManager.Instance.Restart();
        Array.ForEach(_weaponSelectButtons, b => b.onClick.RemoveAllListeners());
    }
}

public enum WeaponType
{
    /// <summary>�U������ </summary>
    Weapon,
    /// <summary>���ʕ���</summary>
    EffectWeapon
}

/// <summary>����̎�� </summary>
public enum Weapons
{
    /// <summary>�i�C�t </summary>
    Knife = 0,
    /// <summary>���@�̏�</summary>
    MagicWand = 1,
    /// <summary>���̏� </summary>
    FireWand = 2,
    /// <summary>����</summary>
    Bible = 3,
    /// <summary>�� </summary>
    Axe = 4,
    /// <summary>�j���j�N </summary>
    Garlic = 5,
    /// <summary>�n�j�� </summary>
    Doll = 6,
}
