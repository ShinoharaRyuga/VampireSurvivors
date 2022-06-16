using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField, Tooltip("武器")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("効果武器")] EffectWeaponBase[] _effectWeapon = default;
    [SerializeField, Tooltip("スキル選択する時に使うUI")] GameObject _skillSelectCanvas = default;
    [SerializeField, Tooltip("スキル選択をする為のボタン")] Button[] _skillSelectButtons = new Button[4];

    /// <summary>プレイヤーが新しい武器を獲得する</summary>
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

    /// <summary>プレイヤーがレベルアップした時呼ばれてプレイヤーが選択する武器を選ぶ </summary>
    public void SetSelectWeapons()
    {
        var selectedWeapons = new List<SkillSelectTable>();
        var totalProb = GameData.SkillSelectTables.Sum(x => x.Probability);
        var rand = UnityEngine.Random.Range(0, totalProb);

        while (selectedWeapons.Count < 4)　  //選択可能な武器を選出する
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

        for (var i = 0; i < 4; i++)　//ボタンに関数を追加する
        {
            var weapon = selectedWeapons[i];
            _skillSelectButtons[i].onClick.AddListener(() => SelectSkill(weapon));
            _skillSelectButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedWeapons[i].Name;
        }
        _skillSelectCanvas.SetActive(true);
    }

    /// <summary>武器選択ボタンのOnClickに追加される </summary>
    /// <param name="index">選択された武器</param>
    /// <param name="type">武器のタイプ</param>
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

        //ifで選択出来ないようにする
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
