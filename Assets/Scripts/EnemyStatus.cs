using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField, Tooltip("体力")] int _hp = 1;
    [SerializeField, Tooltip("攻撃力")] int _attackPower = 1;
    [SerializeField, Tooltip("移動速度")] float _moveSpeed = 1f;
    [SerializeField, Tooltip("死亡時に落とす経験値の値")] int _dropEXPValue = 1;
    [SerializeField, Tooltip("EXPオブジェクト 複製元")] SetEXP _expObj = default;
    Rigidbody2D _rb2D = default;

    public int DropEXPValue { get => _dropEXPValue; }

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var dir = (GameManager.Instance.Player.transform.position - transform.position).normalized * _moveSpeed;
        _rb2D.velocity = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Player.GetDamage(_attackPower);
        }
    }

    /// <summary>ダメージを受ける </summary>
    /// <param name="damage">被ダメージ</param>
    public void GetDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)   //死亡
        {
            GameManager.Instance.ExpSpawner.Instantiate(transform);
            gameObject.SetActive(false);
        }
    }

    public void SetPopPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
