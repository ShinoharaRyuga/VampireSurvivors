using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationNumber : EffectWeaponBase
{
    [SerializeField, Tooltip("")] int _addNumber = 1;
    public override void Effect()
    {
        GameManager.Instance.Player.CharacterStatusArray[4] += _addNumber;
    }
}
