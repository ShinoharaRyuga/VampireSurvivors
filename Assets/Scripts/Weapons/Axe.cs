using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase, IPause
{
    [SerializeField, Tooltip("ŽÎ‚ß•ûŒü‚É”ò‚Î‚·Žž‚ÌÅ‘å’l"), Range(0, 1)] float _maxX = 0;
    [SerializeField, Tooltip("ŽÎ‚ß•ûŒü‚É”ò‚Î‚·Žž‚ÌÅ¬’l"), Range(-1, 0)] float _minX = 0;
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
        Attack(collision, false);
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
        var x = Random.Range(_minX, _maxX);
        var moveDir = new Vector2(x, 1).normalized;
        _rb2D.AddForce(moveDir * MoveSpeed, ForceMode2D.Impulse);
    }

    public void Pause()
    {
        _rb2D.velocity = Vector2.zero;
        _rb2D.gravityScale = 0;
        IsGenerate = false;
    }

    public void Restart()
    {
        _rb2D.gravityScale = 1;
        IsGenerate = true;
    }
}
