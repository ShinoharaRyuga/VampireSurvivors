using System.Collections;
using UnityEngine;

/// <summary>武器の基底クラス </summary>
public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField, Tooltip("次の攻撃までの時間（間隔）")] int _attackInterval = 0;
    [SerializeField, Tooltip("敵に与えるダメージ")] int _damage = 0;
    [SerializeField, Tooltip("ノックバック時にかける力")] int _knockBackPower = 0;
    [SerializeField, Tooltip("移動速度")] int _moveSpeed = 0;
    [SerializeField, Tooltip("生成数")] int _generatorNumber = 1;
    [SerializeField, Tooltip("最大レベル")] int _maxLevel = 9;
    /// <summary>武器のレベル </summary>
    int _currentLevel = 1;
    /// <summary>次の攻撃までの時間(間隔) </summary>
    public int AttackInterval { get => _attackInterval; set => _attackInterval = value; }
    /// <summary>移動速度</summary>
    public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    /// <summary>敵に与えるダメージ </summary>
    public int Damage { get => _damage; set => _damage = value; }

    /// <summary>オブジェクトの動き </summary>
    /// <param name="vector3">進行方向</param>
    public abstract void Move();

    /// <summary>一定間隔ごとに武器を生成する </summary>
    public abstract IEnumerator Generator();

    /// <summary>決められた数武器を生成する </summary>
    /// <param name="weaponObject">生成する武器</param>
    public void GameObjectGenerator()
    {
        for (var i = 0; i < _generatorNumber; i++)
        {
            var offsetX = Random.Range(-1.0f, 1.0f);
            var offsetY = Random.Range(-1.0f, 1.0f);
            var generationPos = new Vector3(GameManager.Instance.Player.transform.position.x + offsetX, GameManager.Instance.Player.transform.position.y + offsetY, 0);
            var go = Instantiate(this, generationPos, Quaternion.identity);
            go.Move();
        }
    }

    /// <summary>敵に当たったらダメージを与える </summary>
    public void Attack(Collider2D other, bool destroyFlag)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();
            enemyStatus.GetDamage(_damage);

            if (destroyFlag)    //敵に当たったら削除される武器なら削除する
            {
                Destroy(gameObject);
            }
        }
    }
}
