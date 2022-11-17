using UnityEngine;

/// <summary>効果武器武器の基底クラス </summary>
public abstract class EffectWeaponBase : MonoBehaviour
{
    [SerializeField, Header("最大レベル")] int _maxLevel = 9;

    /// <summary>現在レベル </summary>
    int _currentLevel = 1;
    /// <summary>現在レベル </summary>
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    /// <summary>武器の効果を発動する </summary>
    public abstract void Effect();

    /// <summary>武器レベルが上がった時の処理 </summary>
    public void LevelUp()
    {
        Effect();
    }
}
