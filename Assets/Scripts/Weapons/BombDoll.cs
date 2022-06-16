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
            collision.GetComponent<EnemyStatus>().GetDamage(Damage + (int)GameManager.Instance.Player.CharacterStatusArray[4]);
            Move();
            //GameManager.Instance.RemovePauseObject(this);
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
        var dir = findEnemy.GetMostNearEnemy() - transform.position;
        _rb2D.AddForce(dir.normalized * MoveSpeed, ForceMode2D.Impulse);
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
