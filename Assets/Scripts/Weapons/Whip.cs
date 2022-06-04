using System.Collections;
using UnityEngine;

public class Whip : WeaponBase
{
    public override IEnumerator Generator()
    {
        var go = Instantiate(gameObject, GameManager.Instance.Player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        go.SetActive(false);

        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            go.transform.position = GameManager.Instance.Player.transform.position;
            go.SetActive(true);
            yield return new WaitForSeconds(1);
            go.SetActive(false);
        }
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
}
