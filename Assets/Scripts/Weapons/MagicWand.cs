using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : WeaponBase, IPause
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

    public override void Move()
    {
        var findEnemy = GameManager.Instance.Player.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetMostNearEnemy() - transform.position;
        transform.up = dir.normalized;
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
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
        GeneratorNumber++;
        AttackInterval -= 0.2f;
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

    public override void ResetStatus()
    {
        GeneratorNumber = 1;
        AttackInterval = 1;
    }
}
