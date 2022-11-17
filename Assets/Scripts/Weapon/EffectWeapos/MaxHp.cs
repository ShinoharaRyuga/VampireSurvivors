using UnityEngine;

/// <summary>Å‘å‘Ì—Í‚ğ‘‚â‚·•Ší </summary>
public class MaxHp : EffectWeaponBase
{
    [SerializeField, Header("‘‚¦‚é—Ê")] int _addHp = 0;

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[0] += _addHp;
    }
}
