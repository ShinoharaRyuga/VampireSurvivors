using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>武器の基底クラス </summary>
public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField, Tooltip("次の攻撃までの時間（間隔）")] int _attackInterval = 0;
    [SerializeField, Tooltip("敵に与えるダメージ")] int _damage = 0;
    [SerializeField, Tooltip("ノックバック時にかける力")] int _knockBackPower = 0;
    [SerializeField, Tooltip("移動速度")] int _moveSpeed = 0;
    [SerializeField, Tooltip("生成数")] int _generatorNumber = 1;

    /// <summary>次の攻撃までの時間(間隔) </summary>
    public int AttackInterval { get => _attackInterval; set => _attackInterval = value; }
    /// <summary>移動速度</summary>
    public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
  
    /// <summary>オブジェクトの動き </summary>
    /// <param name="vector3">進行方向</param>
    public abstract void Move(Vector3 moveDirection);

    /// <summary>一定間隔ごとに武器を生成する </summary>
    /// <param name="playerTransform">プレイヤーの位置</param>
    public abstract IEnumerator Generator(Transform playerTransform);

    /// <summary>決められた数武器を生成する </summary>
    /// <param name="weaponObject">生成する武器</param>
    /// <param name="playerTransform">プレイヤーの位置</param>
    public void GameObjectGenerator(GameObject weaponObject, Transform playerTransform)
    {
        for (var i = 0; i < _generatorNumber; i++)
        {
            var offsetX = Random.Range(-1.0f, 1.0f);
            var offsetY = Random.Range(-1.0f, 1.0f);
            var generationPos = new Vector3(playerTransform.position.x + offsetX, playerTransform.position.y + offsetY, 0);
            var go = Instantiate(this, generationPos, Quaternion.identity);
            go.Move(playerTransform.up);
        }
    }

    /// <summary>敵に当たったらダメージを与える </summary>
    public void Attack(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            enemyStatus.GetDamage(_damage);
        }
    }

    /// <summary>敵に当たったらダメージを与える </summary>
    public void Attack(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();
            enemyStatus.GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
