using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : ObjectPool, IPause
{
    bool _isMove = true;
    int _generationNumber = 1;
    float _generationTime = 2f;
    void Start()
    {
        SetUp();
        StartCoroutine(Generator());
        GameManager.Instance.AddPauseObject(this);
;    }

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
        var popX = Random.Range(-20, 20);
        var popY = Random.Range(-20, 20);
        var popPos = new Vector2(GameManager.Instance.Player.transform.position.x + popX, GameManager.Instance.Player.transform.position.y + popY);
        return popPos;
    }

    IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(_generationTime);
            yield return new WaitUntil(() => _isMove == true);
            EnemyGenerator();
        }
    }

    public void AddEnemyNumber()
    {
        _generationNumber += 2;
        _generationTime -= 0.1f;
    }

    public void Pause()
    {
        _isMove = false;
    }

    public void Restart()
    { 
        _isMove = true;
    }

    private void EnemyGenerator()
    {
        for (var i = 0; i < _generationNumber; i++)
        {
            Instantiate(transform);
        }
    }
}
