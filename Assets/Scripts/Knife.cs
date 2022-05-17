using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBase
{
    Rigidbody2D _rb2D = default;

    public override void Move(Vector3 vector3)
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(vector3 * MoveSpeed, ForceMode2D.Impulse);
    }

    public override IEnumerator Generator(Vector3 vector3)
    {
        var rb2D = GetComponent<Rigidbody2D>();
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            var go = Instantiate(this, transform.position, Quaternion.identity);
            go.Move(vector3);
            Debug.Log("ê∂ê¨");
        }
    }
}
