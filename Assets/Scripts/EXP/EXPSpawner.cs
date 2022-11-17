using UnityEngine;

/// <summary>�o���l�A�C�e���𐶐�����N���X </summary>
public class EXPSpawner : ObjectPool
{
    void Start()
    {
        base.SetUp();
    }

    public override GameObject Spawn(Transform spawnPoint)
    {
        foreach(var target in TargetList)
        {
            if (!target.activeSelf)
            {
                target.SetActive(true);
                target.transform.localPosition = spawnPoint.position;
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
