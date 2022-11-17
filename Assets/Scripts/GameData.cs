using System;
using System.Collections.Generic;


/// <summary>����̏����܂Ƃ߂��N���X</summary>
[Serializable]
public class WeaponSelectTable
{
    /// <summary>����̃^�C�v </summary>
    public WeaponType Type;
    /// <summary>���햼</summary>
    public string Name;
    /// <summary>����ԍ� </summary>
    public int Id;
    /// <summary>���x��</summary>
    public int Level;
    /// <summary>�o��(�I�΂��)�m��</summary>
    public int Probability;
}

public class GameData
{
    /// <summary>�S����̃��X�g</summary>
    static public List<WeaponSelectTable> WeaponSelectTables = new List<WeaponSelectTable>()
    {
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 0, Name = "�i�C�t", Level = 0, Probability = 40 },
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 1, Name = "���@�̏�", Level= 0, Probability = 20},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 2, Name = "���̏�", Level= 0, Probability = 50},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 3, Name = "����", Level= 0, Probability = 30},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 4, Name = "�I�m", Level= 0, Probability = 10},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 5, Name = "�j���j�N", Level= 0, Probability = 20},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 6, Name = "�n�j��", Level = 0, Probability= 30},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 0, Name = "�̗̓A�b�v", Level= 0, Probability = 40},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 1, Name = "������", Level= 0, Probability = 50},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 2, Name = "����", Level= 0, Probability = 60},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 3, Name = "�ړ����x", Level= 0, Probability = 30},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 4, Name = "��", Level= 0, Probability = 10},
    };
}
