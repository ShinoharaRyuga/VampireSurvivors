using UnityEngine;

public class MaxHp : EffectWeaponBase
{
    [SerializeField] int addHp = 0;

    public override void Effect()
    {
        GameManager.Instance.Player.CharacterStatusArray[0] += addHp;
    }
}
