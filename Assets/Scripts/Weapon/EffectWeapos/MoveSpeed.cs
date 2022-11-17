using UnityEngine;

/// <summary>ˆÚ“®‘¬“x‚ªã‚ª‚é•Ší </summary>
public class MoveSpeed : EffectWeaponBase
{
    [SerializeField, Tooltip("’Ç‰Á‚·‚éˆÚ“®‘¬“x")] int _addMoveSpeed = 3; 

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[2] += _addMoveSpeed;
    }
}
