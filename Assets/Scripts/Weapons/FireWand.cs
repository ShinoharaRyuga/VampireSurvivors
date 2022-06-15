using System.Collections;
using UnityEngine;

public class FireWand : WeaponBase, IPause
{
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
        Attack(collision, true);
    }

    public override IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator();
        }
    }

    public override void Move()
    {
        var findEnemy = GameManager.Instance.Player.transform.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetRandomEnemy() - transform.position;
        transform.up = dir.normalized;
        _rb2D.AddForce(dir.normalized * MoveSpeed, ForceMode2D.Impulse);
    }

    public override void LevelUp(int level)
    {
        Debug.Log($"âäÇÃèÒ{level}");
    }

    public void Pause()
    {
        _rb2D.velocity = Vector2.zero;
        IsGenerate = false;
    }

    public void Restart()
    {
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
        IsGenerate = true;
    }
}
