using System.Collections;
using UnityEngine;

public class FireWand : WeaponBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, true);
    }

    public override IEnumerator Generator(Transform playerTransform)
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator(this.gameObject, playerTransform);
        }
    }

    public override void Move(Transform playerTransform)
    {
        var findEnemy = playerTransform.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetRandomEnemy() - transform.position;
        var _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(dir.normalized * MoveSpeed, ForceMode2D.Impulse);
    }
}
