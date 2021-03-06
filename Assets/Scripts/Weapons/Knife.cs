using System.Collections;
using UnityEngine;

/// <summary>???????@?i?C?t </summary>
public class Knife : WeaponBase, IPause
{
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();

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
        GeneratorNumber++;
        AttackInterval -= 0.2f; 
    }

    public void Pause()
    {
        _rb2D.velocity = Vector2.zero;  
        IsGenerate = false;
    }

    public void Restart()
    {
        IsGenerate = true;
        _rb2D.AddForce(transform.up * MoveSpeed, ForceMode2D.Impulse);
    }

    public override void ResetStatus()
    {
        GeneratorNumber = 1;
        AttackInterval = 2;
    }
}
