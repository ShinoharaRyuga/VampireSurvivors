using System.Collections;
using UnityEngine;

public class Whip : WeaponBase
{
    public override IEnumerator Generator(Transform playerTransform)
    {
        var go = Instantiate(gameObject, playerTransform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        go.SetActive(false);

        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            go.transform.position = playerTransform.position;
            go.SetActive(true);
            yield return new WaitForSeconds(1);
            go.SetActive(false);
        }
    }

    public override void Move(Transform playerTransform)
    {
        throw new System.NotImplementedException();
    }
}
