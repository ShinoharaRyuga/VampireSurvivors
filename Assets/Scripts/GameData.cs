using System;
using System.Collections.Generic;


/// <summary>武器の情報をまとめたクラス</summary>
[Serializable]
public class WeaponSelectTable
{
    /// <summary>武器のタイプ </summary>
    public WeaponType Type;
    /// <summary>武器名</summary>
    public string Name;
    /// <summary>武器番号 </summary>
    public int Id;
    /// <summary>レベル</summary>
    public int Level;
    /// <summary>出現(選ばれる)確率</summary>
    public int Probability;
}

public class GameData
{
    /// <summary>全武器のリスト</summary>
    static public List<WeaponSelectTable> WeaponSelectTables = new List<WeaponSelectTable>()
    {
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 0, Name = "ナイフ", Level = 0, Probability = 40 },
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 1, Name = "魔法の杖", Level= 0, Probability = 20},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 2, Name = "炎の杖", Level= 0, Probability = 50},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 3, Name = "聖書", Level= 0, Probability = 30},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 4, Name = "オノ", Level= 0, Probability = 10},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 5, Name = "ニンニク", Level= 0, Probability = 20},
       new WeaponSelectTable(){Type = WeaponType.Weapon, Id = 6, Name = "ハニワ", Level = 0, Probability= 30},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 0, Name = "体力アップ", Level= 0, Probability = 40},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 1, Name = "生成数", Level= 0, Probability = 50},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 2, Name = "磁石", Level= 0, Probability = 60},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 3, Name = "移動速度", Level= 0, Probability = 30},
       new WeaponSelectTable(){Type = WeaponType.EffectWeapon, Id = 4, Name = "回復", Level= 0, Probability = 10},
    };
}
