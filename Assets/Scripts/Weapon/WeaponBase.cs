using System.Collections;
using UnityEngine;

/// <summary>武器の基底クラス </summary>
public abstract class WeaponBase : MonoBehaviour, IPause
{
    /// <summary>攻撃範囲の最小値 </summary>
    const float ATTACK_RANGE_MIN_OFFSET = -1.0f;
    /// <summary>攻撃範囲の最大値 </summary>
    const float ATTACK_RANGE_MAX_OFFSET = 1.0f;

    [SerializeField, Tooltip("次の攻撃までの時間（間隔）")] float _attackInterval = 0;
    [SerializeField, Tooltip("敵に与えるダメージ")] int _damage = 0;
    [SerializeField, Tooltip("ノックバック時にかける力")] int _knockBackPower = 0;
    [SerializeField, Tooltip("移動速度")] int _moveSpeed = 0;
    [SerializeField, Tooltip("生成数")] int _generateNumber = 1;
    [SerializeField, Tooltip("最大レベル")] int _maxLevel = 9;
    [SerializeField, Tooltip("レベルアップ時の強化")] int[] _levelupstatus = default;
    /// <summary>生成をするかどうか </summary>
    static bool _isGenerate = true;
    /// <summary>次の攻撃までの時間(間隔) </summary>
    public float AttackInterval { get => _attackInterval; set => _attackInterval = value; }
    /// <summary>移動速度</summary>
    public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    /// <summary>敵に与えるダメージ </summary>
    public int Damage { get => _damage; }
    /// <summary>生成をするかどうか </summary>
    public static bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }
    /// <summary>最大レベル </summary>
    public int MaxLevel { get => _maxLevel; }
    /// <summary>生成数 </summary>
    public int GenerateNumber { get => _generateNumber; set => _generateNumber = value; }

    /// <summary>オブジェクトの動き </summary>
    public abstract void Move();

    /// <summary>一定間隔ごとに武器を生成する </summary>
    public abstract IEnumerator Generator();
    /// <summary>レベルアップに選ばれた時に呼ばれる </summary>
    public abstract void LevelUp(int level);
    /// <summary>強化したステータスを初期に戻す </summary>
    public abstract void ResetStatus();

    /// <summary>決められた数武器を生成する </summary>
    public void GameObjectGenerator()
    {
        if (!_isGenerate)
        {
            return;
        }

        var generateNumber = _generateNumber + GameManager.Instance.Player.PlayerStatus[4];     //生成数

        for (var i = 0; i < generateNumber; i++)
        {
            var offsetX = Random.Range(ATTACK_RANGE_MIN_OFFSET, ATTACK_RANGE_MAX_OFFSET);
            var offsetY = Random.Range(ATTACK_RANGE_MIN_OFFSET, ATTACK_RANGE_MAX_OFFSET);
            var generationPos = new Vector3(GameManager.Instance.Player.transform.position.x + offsetX, GameManager.Instance.Player.transform.position.y + offsetY, 0);
            var go = Instantiate(this, generationPos, Quaternion.identity);
            go.Move();
        }
    }

    /// <summary>敵に当たったらダメージを与える </summary>
    /// <param name="hitCollider">衝突物</param>
    /// <param name="destroyFlag">攻撃後武器を破壊するかどうか</param>
    public void Attack(Collider2D hitCollider, bool destroyFlag)
    {
        if (hitCollider.CompareTag("Enemy"))
        {
            var enemy = hitCollider.GetComponent<EnemyController>();
            enemy.GetDamage(_damage + (int)GameManager.Instance.Player.PlayerStatus[4]);

            if (destroyFlag)    //敵に当たったら削除される武器なら削除する
            {
                Destroy(gameObject);
                GameManager.Instance.RemovePauseObject(gameObject.GetComponent<IPause>());
            }
        }
    }

    public virtual void Pause()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Restart()
    {
        throw new System.NotImplementedException();
    }
}
