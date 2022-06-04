using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectWeaponBase : MonoBehaviour
{
    [SerializeField] int _maxLevel = 9;
    /// <summary>Œ»İƒŒƒxƒ‹ </summary>
    int _currentLevel = 1;
    /// <summary>Œ»İƒŒƒxƒ‹ </summary>
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    public abstract void Effect();

    public void LevelUp()
    {
        Effect();
    }
}
