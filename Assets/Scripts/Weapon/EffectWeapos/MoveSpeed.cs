using UnityEngine;

/// <summary>�ړ����x���オ�镐�� </summary>
public class MoveSpeed : EffectWeaponBase
{
    [SerializeField, Tooltip("�ǉ�����ړ����x")] int _addMoveSpeed = 3; 

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[2] += _addMoveSpeed;
    }
}
