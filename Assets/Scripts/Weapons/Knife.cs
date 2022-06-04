using System.Collections;
using UnityEngine;

/// <summary>武器種　ナイフ </summary>
public class Knife : WeaponBase
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, true);
    }

    public override void Move()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        transform.up = GameManager.Instance.Player.transform.up;
        rb2D.AddForce(GameManager.Instance.Player.transform.up * MoveSpeed, ForceMode2D.Impulse);
    }

    public override IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator();
        }
    }
}
