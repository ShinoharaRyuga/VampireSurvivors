using TMPro;
using UnityEngine;

/// <summary>初期武器を選択するクラス </summary>
public class SelectFirstWeapon : MonoBehaviour
{
    [SerializeField, Tooltip("0=ナイフ 1=魔法の杖　2=炎の杖　3=聖書　4=斧　5=ニンニク　6=ハニワ")] Weapons _firstWeapon = Weapons.Knife;
    [SerializeField, Tooltip("初期武器を表示するテキスト")] TextMeshProUGUI _firstWeaponText = default;
    [SerializeField, Tooltip("使用武器の名前")] string _weaponName = "";

    /// <summary>
    /// 武器を選択する
    /// ボタンが押されたら呼び出される
    /// </summary>
    public void SelectWeapon()
    {
        GameManager.Instance.FirstWeapon = _firstWeapon;
        _firstWeaponText.text = $"使用武器:{_weaponName}";
    }
}
