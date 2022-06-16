using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField, Tooltip("����")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("���ʕ���")] EffectWeaponBase[] _effectWeapon = default;
    [SerializeField, Tooltip("�X�L���I�����鎞�Ɏg��UI")] GameObject _skillSelectCanvas = default;
    [SerializeField, Tooltip("�X�L���I��������ׂ̃{�^��")] Button[] _skillSelectButtons = new Button[4];

    /// <summary>�v���C���[���V����������l������</summary>
    /// <param name="index"></param>
    /// <param name="type"></param>
    public void GetWeapon(int index, WeaponType type)
    {
        GameManager.Instance.Player.SetWeaponIndex(index, type);
        if (type == WeaponType.Weapon)
        {
            StartCoroutine(_weapons[index].Generator());
            Debug.Log(_weapons[index]);
            //  var go = Instantiate(_weapons[5], GameManager.Instance.Player.transform);
            //  StartCoroutine(go.Generator());
        }
        else
        {
            _effectWeapon[index].Effect();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var data in GameData.SkillSelectTables)
            {
                Debug.Log($"{data.Name} {data.Level}");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(_weapons[0].MaxLevel);
        }
    }

    /// <summary>�v���C���[�����x���A�b�v�������Ă΂�ăv���C���[���I�����镐���I�� </summary>
    public void SetSelectWeapons()
    {
        var selectedWeapons = new List<SkillSelectTable>();
        var totalProb = GameData.SkillSelectTables.Sum(x => x.Probability);
        var rand = UnityEngine.Random.Range(0, totalProb);

        while (selectedWeapons.Count < 4)�@  //�I���\�ȕ����I�o����
        {
            foreach (var data in GameData.SkillSelectTables)
            {
                if (rand < data.Probability && !selectedWeapons.Contains(data) &&  data.Level < _weapons[data.Id].MaxLevel)
                {
                    selectedWeapons.Add(data);
                    rand = UnityEngine.Random.Range(0, totalProb);
                    break;
                }
                rand -= data.Probability;
            }
        }

        for (var i = 0; i < 4; i++)�@//�{�^���Ɋ֐���ǉ�����
        {
            var weapon = selectedWeapons[i];
            _skillSelectButtons[i].onClick.AddListener(() => SelectSkill(weapon));
            _skillSelectButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedWeapons[i].Name;
        }
        _skillSelectCanvas.SetActive(true);
    }

    /// <summary>����I���{�^����OnClick�ɒǉ������ </summary>
    /// <param name="index">�I�����ꂽ����</param>
    /// <param name="type">����̃^�C�v</param>
    private void SelectSkill(SkillSelectTable skill)
    {
        skill.Level++;
        if (skill.Level == 1) 
        {
            GetWeapon(skill.Id, skill.Type);
            Debug.Log(skill.Name);
        }
        else
        {
            if (skill.Type == WeaponType.Weapon)
            {
                _weapons[skill.Id].LevelUp(skill.Level);
            }
            else
            {
                //   _effectWeapon[index].LevelUp(level);
            }
        }

        //if�őI���o���Ȃ��悤�ɂ���
        _skillSelectCanvas.SetActive(false);
        GameManager.Instance.Restart();
        Array.ForEach(_skillSelectButtons, b => b.onClick.RemoveAllListeners());
    }
}

public enum WeaponType
{
    Weapon,
    EffectWeapon
}
