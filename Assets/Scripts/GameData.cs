using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class SkillSelectTable
{
    public WeaponType Type;
    public string Name;
    public int Id;
    public int Level;
    public int Probability;
}

public class GameData
{
    static public List<SkillSelectTable> SkillSelectTables = new List<SkillSelectTable>()
    {
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 0, Name = "�i�C�t", Level = 0, Probability = 40 },
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 1, Name = "���@�̏�", Level= 0, Probability = 900},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 2, Name = "���̏�", Level= 0, Probability = 50},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 3, Name = "����", Level= 0, Probability = 30},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 4, Name = "�I�m", Level= 0, Probability = 10},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 5, Name = "�j���j�N", Level= 0, Probability = 20},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 0, Name = "�̗̓A�b�v", Level= 0, Probability = 40},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 1, Name = "�З�", Level= 0, Probability = 40},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 2, Name = "������", Level= 0, Probability = 50},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 3, Name = "����", Level= 0, Probability = 60},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 4, Name = "�ړ����x", Level= 0, Probability = 30},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 5, Name = "��", Level= 0, Probability = 10},
    };
}
