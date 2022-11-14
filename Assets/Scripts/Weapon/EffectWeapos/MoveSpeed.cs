using UnityEngine;

public class MoveSpeed : EffectWeaponBase
{
    [SerializeField, Tooltip("�ǉ�����ړ����x")] int _addMoveSpeed = 3; 
    public override void Effect()
    {
        GameManager.Instance.Player.CharacterStatusArray[2] += _addMoveSpeed;
    }
}
