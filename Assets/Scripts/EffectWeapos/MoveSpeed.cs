using UnityEngine;

public class MoveSpeed : EffectWeaponBase
{
    [SerializeField, Tooltip("追加する移動速度")] int _addMoveSpeed = 3; 
    public override void Effect()
    {
        GameManager.Instance.Player.CharacterStatusArray[2] += _addMoveSpeed;
    }
}
