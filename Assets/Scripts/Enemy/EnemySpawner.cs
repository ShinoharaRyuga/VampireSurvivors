using System.Collections;
using UnityEngine;

/// <summary>敵を生成するクラス </summary>
public class EnemySpawner : ObjectPool, IPause
{
    /// <summary>スポーン時の最小値 </summary>
    const float SPAWN_POINT_MIN_OFFSET = -20f;
    /// <summary>スポーン時の最大値 </summary>
    const float SPAWN_POINT_MAX_OFFSET = -20f;
    /// <summary>敵スポーン時の体力 </summary>
    const int ENEMY_SPAWN_HP = 10;

    [SerializeField, Tooltip("増やす敵の数")] int _addEnemy = 5;
    [SerializeField, Tooltip("生成時間減少")] float _generationTimer = 0.2f;
    /// <summary>生成をしてもよいか </summary>
    bool _isGenerate = true;
    /// <summary>一度で生成する敵の数 </summary>
    int _generationNumber = 1;
    /// <summary>生成するまでの待ち時間</summary>
    float _generationWaitTime = 2f;

    void Start()
    {
        SetUp();
        StartCoroutine(Generator());    //生成を開始する
        GameManager.Instance.AddPauseObject(this);
    }

    public override GameObject Spawn(Vector2 spawnPoint)
    {
        foreach (var target in TargetList)
        {
            if (!target.activeSelf)
            {
                var enemyStatus = target.GetComponent<EnemyController>();
                enemyStatus.SetSpawnPoint(spawnPoint);
                enemyStatus.Hp = ENEMY_SPAWN_HP;
                GameManager.Instance.AddPauseObject(enemyStatus);   //一時停止させるオブジェクトのリストに追加
                target.SetActive(true);
                return target;
            }
        }

        return null;
    }

    public override Vector2 SetSpawnPoint()
    {
        var popX = Random.Range(SPAWN_POINT_MIN_OFFSET, SPAWN_POINT_MAX_OFFSET);
        var popY = Random.Range(SPAWN_POINT_MIN_OFFSET, SPAWN_POINT_MAX_OFFSET);
        var spawnPoint = new Vector2(GameManager.Instance.Player.transform.position.x + popX, GameManager.Instance.Player.transform.position.y + popY);
        return spawnPoint;
    }

    /// <summary>スポナーを強化する </summary>
    public void UpgradeSpawner()
    {
        _generationNumber += _addEnemy;     //一度で生成する敵の数を増やす
        _generationWaitTime -= _generationTimer;    //生成するまで時間を短くする
    }

    public void Pause()
    {
        _isGenerate = false;
    }

    public void Restart()
    {
        _isGenerate = true;
    }

    /// <summary>敵を一定の数生成する </summary>
    private void EnemyGenerator()
    {
        for (var i = 0; i < _generationNumber; i++)
        {
            Spawn(SetSpawnPoint());
        }
    }

    /// <summary>一定時間後に敵の生成を行う </summary>
    IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(_generationWaitTime);
            yield return new WaitUntil(() => _isGenerate == true);  //一時停止中であれば待つ
            EnemyGenerator();
        }
    }
}
