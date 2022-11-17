using System.Collections;
using UnityEngine;

/// <summary>武器種　ナイフ </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Knife : WeaponBase
{
    /// <summary>レベルアップ時に攻撃間隔が短くなる </summary>
    const float SHORT_ATTACK_INTERVAL_TIME = 0.2f;
    /// <summary>初期攻撃の値 </summary>
    const float FIRST_ATTACK_VALUE = 1f;

    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();

    private void OnBecameInvisible()
    {
        GameManager.Instance.RemovePauseObject(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, true);
    }

    public override void Move()
    {
        GameManager.Instance.AddPauseObject(this);
        transform.up = GameManager.Instance.Player.transform.up;
        _rb2D.AddForce(GameManager.Instance.Player.transform.up * MoveSpeed, ForceMode2D.Impulse);
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
        IsGenerate = true;
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
    }

    public override void ResetStatus()
    {
        GenerateNumber = (int)FIRST_ATTACK_VALUE;
        AttackInterval = FIRST_ATTACK_VALUE;
    }
}
