using UnityEngine;

/// <summary>経験値アイテムを取得できる範囲が広がる武器 </summary>
public class Magnet : EffectWeaponBase
{
    [SerializeField, Tooltip("引き寄せ可能エリアを増やす")] float _addArea = 0;

    public override void Effect()
    {
        GameManager.Instance.Player.PlayerStatus[3] += _addArea;
    }
}
