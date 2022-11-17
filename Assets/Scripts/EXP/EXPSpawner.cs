using UnityEngine;

/// <summary>経験値アイテムを生成するクラス </summary>
public class EXPSpawner : ObjectPool
{
    void Start()
    {
       SetUp();
    }

    public override GameObject Spawn(Vector2 spawnPoint)
    {
        foreach(var target in TargetList)
        {
            if (!target.activeSelf)
            {
                target.SetActive(true);
                target.transform.localPosition = spawnPoint;
                return target;
            }
        }

        return null;
    }

    public override Vector2 SetSpawnPoint()
    {
        return Vector2.zero;
    }
}
