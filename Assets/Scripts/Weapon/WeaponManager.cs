using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>武器関連の処理を管理するクラス </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField, Tooltip("0=ナイフ 1=魔法の杖　2=炎の杖　3=聖書　4=斧　5=ニンニク　6=ハニワ")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("0=最大体力 1=生成数 2=磁石 3=移動速度 4=回復")] EffectWeaponBase[] _effectWeapon = default;
    [SerializeField, Tooltip("武器選択する時に使うUI")] GameObject _weaponSelectCanvas = default;
    [SerializeField, Tooltip("武器選択をする為のボタン")] Button[] _weaponSelectButtons = new Button[4];

    /// <summary>プレイヤーが新しい武器を獲得する</summary>
    /// <param name="index"></param>
    /// <param name="type"></param>
    public void GetWeapon(int index, WeaponType type)
    {
        if (type == WeaponType.Weapon)
        {
            if (index == 5)　 //ニンニクの攻撃を開始する
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

    /// <summary>全武器の性能を初期化する </summary>
    public void ResetWeapons()
    {
        foreach (var weapon in _weapons)
        {
            weapon.GeneratorNumber = 1;
            weapon.ResetStatus();
        }
    }

    /// <summary>プレイヤーがレベルアップした時呼ばれてプレイヤーが選択する武器を選ぶ </summary>
    public void SetSelectWeapons()
    {
        var selectedWeapons = new List<WeaponSelectTable>();
        var totalProb = GameData.WeaponSelectTables.Sum(x => x.Probability);
        var rand = UnityEngine.Random.Range(0, totalProb);

        while (selectedWeapons.Count < _weaponSelectButtons.Length)　  //選択可能な武器を選出する
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

        for (var i = 0; i < _weaponSelectButtons.Length; i++)　//ボタンに関数を追加する
        {
            var weapon = selectedWeapons[i];
            _weaponSelectButtons[i].onClick.AddListener(() => SelectWeapon(weapon));
            _weaponSelectButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedWeapons[i].Name;
        }
        _weaponSelectCanvas.SetActive(true);
    }

    /// <summary>武器選択ボタンのOnClickに追加される </summary>
    private void SelectWeapon(WeaponSelectTable weapon)
    {
        weapon.Level++;
        if (weapon.Level == 1)  //新しい武器を獲得
        {
            GetWeapon(weapon.Id, weapon.Type);
        }
        else　　　　　　　　　　//武器のレベルを上げる
        {
            if (weapon.Type == WeaponType.Weapon)　//攻撃武器
            {
                _weapons[weapon.Id].LevelUp(weapon.Level);
            }
            else　　　　　　　　　　　　　　　　　//効果武器
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
    /// <summary>攻撃武器 </summary>
    Weapon,
    /// <summary>効果武器</summary>
    EffectWeapon
}

/// <summary>武器の種類 </summary>
public enum Weapons
{
    /// <summary>ナイフ </summary>
    Knife = 0,
    /// <summary>魔法の杖</summary>
    MagicWand = 1,
    /// <summary>炎の杖 </summary>
    FireWand = 2,
    /// <summary>聖書</summary>
    Bible = 3,
    /// <summary>斧 </summary>
    Axe = 4,
    /// <summary>ニンニク </summary>
    Garlic = 5,
    /// <summary>ハニワ </summary>
    Doll = 6,
}
