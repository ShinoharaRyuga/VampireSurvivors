using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField, Tooltip("•Ší")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("Œø‰Ê•Ší")] EffectWeaponBase[] _effectWeapon = default;
 
    public void GetWeapon(int index, WeaponType type)
    {
        GameManager.Instance.Player.SetWeaponIndex(index, type);
        StartCoroutine(_weapons[index].Generator(GameManager.Instance.Player.transform));
    }
}

public enum WeaponType
{
    Weapon,
    EffectWeapon
}
