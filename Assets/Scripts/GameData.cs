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
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 0, Name = "�i�C�t", Level = 0, Probability = 10 },
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 1, Name = "���@�̏�", Level= 0, Probability = 70},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 2, Name = "���̏�", Level= 0, Probability = 50},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 3, Name = "����", Level= 0, Probability = 80},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 4, Name = "�I�m", Level= 0, Probability = 60},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 5, Name = "�j���j�N", Level= 0, Probability = 20}
    };
}
