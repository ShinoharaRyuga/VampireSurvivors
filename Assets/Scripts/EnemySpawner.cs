using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : ObjectPool
{
    void Start()
    {
        SetUp();
        StartCoroutine(Generator());
    }

    public override GameObject Instantiate(Transform pos)
    {
        foreach (var target in TargetList)
        {
            if (!target.activeSelf)
            {
                target.GetComponent<EnemyStatus>().SetPopPosition(SetPopPos());
                target.SetActive(true);
                return target;
            }
        }

        return null;
    }

    public override Vector2 SetPopPos()
    {
        var popX = Random.Range(-10, 10);
        var popY = Random.Range(-10, 10);
        var popPos = new Vector2(GameManager.Instance.Player.transform.position.x + popX, GameManager.Instance.Player.transform.position.y + popY);
        return popPos;

        //var rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //var leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(transform);
        }
    }
}
