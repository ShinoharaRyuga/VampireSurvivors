using System.Collections;
using UnityEngine;

/// <summary>•Šíí@‰Š‚Ìñ </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class FireWand : WeaponBase
{
    /// <summary>‰Šú¶¬” </summary>
    const int FIRST_GENERATE_NUMBER = 1;

    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();

    void Start()
    {
        GameManager.Instance.AddPauseObject(this);
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.RemovePauseObject(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, true);
    }

    public override IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator();
        }
    }

    public override void Move()
    {
        var findEnemy = GameManager.Instance.Player.transform.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetRandomEnemy() - transform.position;  //is•ûŒü‚ğæ“¾
        transform.up = dir.normalized;
        _rb2D.AddForce(dir.normalized * MoveSpeed, ForceMode2D.Impulse);
    }

    public override void LevelUp(int level)
    {
        GenerateNumber++;
    }

    public override void ResetStatus()
    {
        GenerateNumber = FIRST_GENERATE_NUMBER;
    }

    public override void Pause()
    {
        _rb2D.velocity = Vector2.zero;
        IsGenerate = false;
    }

    public override void Restart()
    {
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
        IsGenerate = true;
    }
}
