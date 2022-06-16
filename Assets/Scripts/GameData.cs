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
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 0, Name = "ナイフ", Level = 0, Probability = 40 },
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 1, Name = "魔法の杖", Level= 0, Probability = 900},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 2, Name = "炎の杖", Level= 0, Probability = 50},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 3, Name = "聖書", Level= 0, Probability = 30},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 4, Name = "オノ", Level= 0, Probability = 10},
       new SkillSelectTable(){Type = WeaponType.Weapon, Id = 5, Name = "ニンニク", Level= 0, Probability = 20},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 0, Name = "体力アップ", Level= 0, Probability = 40},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 1, Name = "威力", Level= 0, Probability = 40},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 2, Name = "生成数", Level= 0, Probability = 50},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 3, Name = "磁石", Level= 0, Probability = 60},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 4, Name = "移動速度", Level= 0, Probability = 30},
       new SkillSelectTable(){Type = WeaponType.EffectWeapon, Id = 5, Name = "回復", Level= 0, Probability = 10},
    };
}
