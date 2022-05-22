using System.Collections;
using UnityEngine;

public class Bible : WeaponBase
{
    public override IEnumerator Generator(Transform playerTransform)
    {
        var go = Instantiate(gameObject, new Vector2(playerTransform.position.x + 3, playerTransform.position.y), Quaternion.identity);

        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
           
        }
    }

    public override void Move(Transform playerTransform)
    {
        throw new System.NotImplementedException();
    }
}
