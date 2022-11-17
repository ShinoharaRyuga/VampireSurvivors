using System.Collections;
using UnityEngine;

/// <summary>武器 斧のクラス </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Axe : WeaponBase
{
    /// <summary>初期生成数 </summary>
    const int FIRST_GENARATE_NUMBER = 1;

    [SerializeField, Tooltip("斜め方向に飛ばす時の最大値"), Range(0, 1)] float _maxX = 0;
    [SerializeField, Tooltip("斜め方向に飛ばす時の最小値"), Range(-1, 0)] float _minX = 0;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();

    void Start()
    {
        GameManager.Instance.AddPauseObject(this);
    }
   
    private void OnBecameInvisible()
    {
        GameManager.Instance.RemovePauseObject(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, false);
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
    }

    public override void ResetStatus()
    {
        GenerateNumber = FIRST_GENARATE_NUMBER;
    }

    public override void Move()
    {
        var x = Random.Range(_minX, _maxX);
        var moveDir = new Vector2(x, Vector2.up.y).normalized;
        _rb2D.AddForce(moveDir * MoveSpeed, ForceMode2D.Impulse);
    }

    public override void Pause()
    {
        _rb2D.velocity = Vector2.zero;
        _rb2D.gravityScale = 0;
        IsGenerate = false;
    }

    public override void Restart()
    {
        _rb2D.gravityScale = 1;
        IsGenerate = true;
    }
}
