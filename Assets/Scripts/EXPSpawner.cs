using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPSpawner : ObjectPool
{
    void Start()
    {
        SetUp();
    }

    public override GameObject Instantiate(Transform pos)
    {
        foreach(var target in TargetList)
        {
            if (!target.activeSelf)
            {
                target.SetActive(true);
                target.transform.localPosition = pos.position;
                return target;
            }
        }

        return null;
    }

    public override Vector2 SetPopPos()
    {
        throw new System.NotImplementedException();
    }
}
