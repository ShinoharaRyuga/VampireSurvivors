using System.Collections;
using UnityEngine;

/// <summary>�����@�i�C�t </summary>
public class Knife : WeaponBase
{
    public override void Move(Vector3 vector3)
    {
        var rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(vector3 * MoveSpeed, ForceMode2D.Impulse);
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
