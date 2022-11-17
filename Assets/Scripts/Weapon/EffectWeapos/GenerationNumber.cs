using UnityEngine;

/// <summary>•Ší¶¬”‚ğ‘‚â‚·•Ší </summary>
public class GenerationNumber : EffectWeaponBase
{
    [SerializeField, Tooltip("‘‚â‚·—Ê")] int _addNumber = 1;

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[4] += _addNumber;
    }
}
