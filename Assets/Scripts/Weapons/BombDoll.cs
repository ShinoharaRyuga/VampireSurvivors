using System.Collections;
using UnityEngine;

public class BombDoll : WeaponBase, IPause
{
    [SerializeField, Tooltip("Õ“Ë‚³‚¹‚éƒŒƒCƒ„[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("õ“G")] float _radius = 1f;
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
            Attack();
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
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        var findEnemy = GameManager.Instance.Player.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetRandomEnemy() - transform.position;
        transform.up = dir.normalized;
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
    }

    public void Attack()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayerMask);
     
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyStatus>().GetDamage(Damage + (int)GameManager.Instance.Player.CharacterStatusArray[4]);
            Debug.Log(Damage + (int)GameManager.Instance.Player.CharacterStatusArray[4]);
        }

        Destroy(gameObject);
    }

    public void Pause()
    {
        IsGenerate = false;
        _rb2D.velocity = Vector2.zero;
    }

    public void Restart()
    {
        IsGenerate = true;
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
    }
}
