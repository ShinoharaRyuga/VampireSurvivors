using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : WeaponBase
{
    FindEnemy _findEnemy = default;
    public override void Move(Transform playerTransform)
    {
        _findEnemy = playerTransform.gameObject.GetComponent<FindEnemy>();
        var dir = _findEnemy.GetMostNearEnemy() - transform.position;
        var _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(dir * MoveSpeed, ForceMode2D.Impulse);
    }

    public override IEnumerator Generator(Transform playerTransform)
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator(this.gameObject, playerTransform);
        }
    }

    public void GetFindEnemy(FindEnemy findEnemy)
    {
        _findEnemy = findEnemy;
    }
}
