using UnityEngine;

public class AttackPower : EffectWeaponBase
{
    [SerializeField] int _addAttackPower = 2; 
    public override void Effect()
    {
        GameManager.Instance.Player.CharacterStatusArray[4] += _addAttackPower;
    }
}
