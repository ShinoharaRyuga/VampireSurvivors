using UnityEngine;

/// <summary>���ʕ��핐��̊��N���X </summary>
public abstract class EffectWeaponBase : MonoBehaviour
{
    [SerializeField, Header("�ő僌�x��")] int _maxLevel = 9;

    /// <summary>���݃��x�� </summary>
    int _currentLevel = 1;
    /// <summary>���݃��x�� </summary>
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    /// <summary>����̌��ʂ𔭓����� </summary>
    public abstract void Effect();

    /// <summary>���탌�x�����オ�������̏��� </summary>
    public void LevelUp()
    {
        Effect();
    }
}
