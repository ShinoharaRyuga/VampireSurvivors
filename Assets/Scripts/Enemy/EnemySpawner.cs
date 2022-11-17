using System.Collections;
using UnityEngine;

/// <summary>�G�𐶐�����N���X </summary>
public class EnemySpawner : ObjectPool, IPause
{
    /// <summary>�X�|�[�����̍ŏ��l </summary>
    const float SPAWN_POINT_MIN_OFFSET = -20f;
    /// <summary>�X�|�[�����̍ő�l </summary>
    const float SPAWN_POINT_MAX_OFFSET = -20f;
    /// <summary>�G�X�|�[�����̗̑� </summary>
    const int ENEMY_SPAWN_HP = 10;

    [SerializeField, Tooltip("���₷�G�̐�")] int _addEnemy = 5;
    [SerializeField, Tooltip("�������Ԍ���")] float _generationTimer = 0.2f;
    /// <summary>���������Ă��悢�� </summary>
    bool _isGenerate = true;
    /// <summary>��x�Ő�������G�̐� </summary>
    int _generationNumber = 1;
    /// <summary>��������܂ł̑҂�����</summary>
    float _generationWaitTime = 2f;

    void Start()
    {
        SetUp();
        StartCoroutine(Generator());    //�������J�n����
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
                GameManager.Instance.AddPauseObject(enemyStatus);   //�ꎞ��~������I�u�W�F�N�g�̃��X�g�ɒǉ�
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

    /// <summary>�X�|�i�[���������� </summary>
    public void UpgradeSpawner()
    {
        _generationNumber += _addEnemy;     //��x�Ő�������G�̐��𑝂₷
        _generationWaitTime -= _generationTimer;    //��������܂Ŏ��Ԃ�Z������
    }

    public void Pause()
    {
        _isGenerate = false;
    }

    public void Restart()
    {
        _isGenerate = true;
    }

    /// <summary>�G�����̐��������� </summary>
    private void EnemyGenerator()
    {
        for (var i = 0; i < _generationNumber; i++)
        {
            Spawn(SetSpawnPoint());
        }
    }

    /// <summary>��莞�Ԍ�ɓG�̐������s�� </summary>
    IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(_generationWaitTime);
            yield return new WaitUntil(() => _isGenerate == true);  //�ꎞ��~���ł���Α҂�
            EnemyGenerator();
        }
    }
}
