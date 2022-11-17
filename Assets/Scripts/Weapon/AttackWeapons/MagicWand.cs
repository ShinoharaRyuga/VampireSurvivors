using System.Collections;
using UnityEngine;

/// <summary>•Šíí –‚–@‚Ìñ </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MagicWand : WeaponBase
{
    /// <summary>ƒŒƒxƒ‹ƒAƒbƒv‚ÉUŒ‚ŠÔŠu‚ª’Z‚­‚È‚é </summary>
    const float SHORT_ATTACK_INTERVAL_TIME = 0.2f;
    /// <summary>‰ŠúUŒ‚‚Ì’l </summary>
    const float FIRST_ATTACK_VALUE = 1f;

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

    public override void Move()
    {
        var findEnemy = GameManager.Instance.Player.gameObject.GetComponent<FindEnemy>();
        var dir = findEnemy.GetMostNearEnemy() - transform.position;
        transform.up = dir.normalized;
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
    }

    public override IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator();
        }
    }

    public override void LevelUp(int level)
    {
        GenerateNumber++;
        AttackInterval -= SHORT_ATTACK_INTERVAL_TIME;
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

    public override void ResetStatus()
    {
        GenerateNumber = (int)FIRST_ATTACK_VALUE;
        AttackInterval = FIRST_ATTACK_VALUE;
    }
}
