using UnityEngine;

/// <summary>�ő�̗͂𑝂₷���� </summary>
public class MaxHp : EffectWeaponBase
{
    [SerializeField, Header("�������")] int _addHp = 0;

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[0] += _addHp;
    }
}
