using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : EffectWeaponBase
{
    [SerializeField, Tooltip("�����񂹉\�G���A�𑝂₷")] float _addArea = 0;
    public override void Effect()
    {
        GameManager.Instance.Player.CharacterStatusArray[3] += _addArea;
    }
}
