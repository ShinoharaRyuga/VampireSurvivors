using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : WeaponBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, true);
    }

    public override void Move(Transform playerTransform)
    {
        var findEnemy = playerTransform.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetMostNearEnemy() - transform.position;
        var _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(dir.normalized * MoveSpeed, ForceMode2D.Impulse);
    }

    public override IEnumerator Generator(Transform playerTransform)
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator(this.gameObject, playerTransform);
        }
    }
}
