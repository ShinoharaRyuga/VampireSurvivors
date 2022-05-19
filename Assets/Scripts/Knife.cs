using System.Collections;
using UnityEngine;

/// <summary>武器種　ナイフ </summary>
public class Knife : WeaponBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }

    public override void Move(Vector3 moveDirection)
    {
        var rb2D = GetComponent<Rigidbody2D>();
        transform.up = moveDirection;
        rb2D.AddForce(moveDirection * MoveSpeed, ForceMode2D.Impulse);
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
