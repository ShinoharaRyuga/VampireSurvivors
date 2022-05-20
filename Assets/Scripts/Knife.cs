using System.Collections;
using UnityEngine;

/// <summary>�����@�i�C�t </summary>
public class Knife : WeaponBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }

    public override void Move(Transform playerTransform)
    {
        var rb2D = GetComponent<Rigidbody2D>();
        transform.up = playerTransform.up;
        rb2D.AddForce(playerTransform.up * MoveSpeed, ForceMode2D.Impulse);
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
