using UnityEngine;

/// <summary>�o���l�A�C�e�����擾�ł���͈͂��L���镐�� </summary>
public class Magnet : EffectWeaponBase
{
    [SerializeField, Tooltip("�����񂹉\�G���A�𑝂₷")] float _addArea = 0;

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[3] += _addArea;
    }
}
