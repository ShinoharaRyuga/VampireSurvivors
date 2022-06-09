using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField, Tooltip("•Ší")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("Œø‰Ê•Ší")] EffectWeaponBase[] _effectWeapon = default;

    private void Start()
    {
        var go = Instantiate(_weapons[5], GameManager.Instance.Player.transform);
        StartCoroutine(go.Generator());
    }

    public void GetWeapon(int index, WeaponType type)
    {
        GameManager.Instance.Player.SetWeaponIndex(index, type);
        StartCoroutine(_weapons[index].Generator());
    }
}

public enum WeaponType
{
    Weapon,
    EffectWeapon
}
