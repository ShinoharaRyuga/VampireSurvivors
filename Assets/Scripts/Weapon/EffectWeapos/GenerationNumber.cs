using UnityEngine;

/// <summary>���퐶�����𑝂₷���� </summary>
public class GenerationNumber : EffectWeaponBase
{
    [SerializeField, Tooltip("���₷��")] int _addNumber = 1;

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[4] += _addNumber;
    }
}
