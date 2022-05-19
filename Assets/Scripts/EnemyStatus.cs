using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField, Tooltip("体力")] int _hp = 1;
    [SerializeField, Tooltip("攻撃力")] int _attackPower = 1;
    [SerializeField, Tooltip("移動速度")] float _moveSpeed = 1f;
    [SerializeField, Tooltip("死亡時に落とす経験値の値")] int _dropEXPValue = 1;
    [SerializeField, Tooltip("プレイヤー 攻撃対象")] GameObject _player = default;
    [SerializeField, Tooltip("EXPオブジェクト 複製元")] SetEXP _expObj = default;

    Rigidbody2D _rb2D = default;
    PlayerController _playerController => _player.GetComponent<PlayerController>();
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var dir = (_player.transform.position - transform.position).normalized * _moveSpeed;
        _rb2D.velocity = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.GetDamage(_attackPower);
        }
    }

    /// <summary>ダメージを受ける </summary>
    /// <param name="damage">被ダメージ</param>
    public void GetDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            var go = Instantiate(_expObj, transform.position, Quaternion.identity);
            go.AddEXP = _dropEXPValue;
            Destroy(gameObject);
        }
    }
}
