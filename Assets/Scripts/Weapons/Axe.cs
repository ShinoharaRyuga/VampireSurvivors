using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase
{
    [SerializeField, Tooltip("Î‚ß•ûŒü‚É”ò‚Î‚·‚ÌÅ‘å’l"), Range(0, 1)] float _maxX = 0;
    [SerializeField, Tooltip("Î‚ß•ûŒü‚É”ò‚Î‚·‚ÌÅ¬’l"), Range(-1, 0)] float _minX = 0;

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
        var rb2D = GetComponent<Rigidbody2D>();
        var x = Random.Range(_minX, _maxX);
        var moveDir = new Vector2(x, 1).normalized;
        rb2D.AddForce(moveDir * MoveSpeed, ForceMode2D.Impulse);
    }
}
