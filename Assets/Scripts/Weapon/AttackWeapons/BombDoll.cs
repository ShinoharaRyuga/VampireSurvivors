using System.Collections;
using UnityEngine;

/// <summary>�����@�n�j�� </summary>
[RequireComponent(typeof(Rigidbody))]
public class BombDoll : WeaponBase
{
    /// <summary>�U���\�� </summary>
    const int ATTACK_COUNT = 3;
    /// <summary>���������� </summary>
    const int FIRST_GENERATE_NUMBER = 1;

    [SerializeField, Tooltip("�Փ˂����郌�C���[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("���G�͈�")] float _radius = 1f;
    /// <summary>�G�ɓ��������� </summary>
    int _hitCount = 0;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();

    void Start()
    {
        GameManager.Instance.AddPauseObject(this);
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.RemovePauseObject(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().GetDamage(Damage);
            Move();
            _hitCount++;
        }

        if (_hitCount >= ATTACK_COUNT)
        {
            Destroy(gameObject);
            GameManager.Instance.RemovePauseObject(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public override IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator();
        }
    }

    public override void LevelUp(int level)
    {
        GenerateNumber++;
    }

    public override void Move()
    {
        var findEnemy = GameManager.Instance.Player.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetMostNearEnemy() - transform.position;
        _rb2D.AddForce(dir.normalized * MoveSpeed, ForceMode2D.Impulse);
    }

    public override void Pause()
    {
        IsGenerate = false;
        _rb2D.velocity = Vector2.zero;
    }

    public override void Restart()
    {
        IsGenerate = true;
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
    }

    public override void ResetStatus()
    {
        GenerateNumber = FIRST_GENERATE_NUMBER;
    }
}
