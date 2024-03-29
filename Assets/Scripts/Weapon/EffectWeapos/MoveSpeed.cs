using UnityEngine;

/// <summary>移動速度が上がる武器 </summary>
public class MoveSpeed : EffectWeaponBase
{
    [SerializeField, Tooltip("追加する移動速度")] int _addMoveSpeed = 3; 

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[2] += _addMoveSpeed;
    }
}
