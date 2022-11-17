using System.Collections;
using UnityEngine;

/// <summary>武器種　聖書 </summary>
public class Bible : WeaponBase
{
    /// <summary>移動関連で使用する </summary>
    const float MOVE_VALUE = 3f;
    /// <summary>レベルアップ時に増える攻撃可能時間 </summary>
    const float ADD_ACTIVE_TIME = 0.2f;
    /// <summary>初期攻撃可能時間 </summary>
    const float FIRST_ACTIVE_TIME = 1f;

　　/// <summary>攻撃可能時間 </summary>
    float _activeTime = FIRST_ACTIVE_TIME;
    /// <summary>移動可能かどうか </summary>
    bool _isMove = true;
    Transform _playerTransform = default;
    Rigidbody2D _rb2D = default;

    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public Rigidbody2D Rb2D { get => _rb2D; set => _rb2D = value; }

    void Start()
    {
        GameManager.Instance.AddPauseObject(this);
    }

    private void Update()
    {
        if (_isMove)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, false);
    }

    public override IEnumerator Generator()
    {
        var go = Instantiate(gameObject, new Vector2(GameManager.Instance.Player.transform.position.x + MOVE_VALUE, GameManager.Instance.Player.transform.position.y), Quaternion.identity);
        go.GetComponent<Bible>().Rb2D = go.GetComponent<Rigidbody2D>();
        go.GetComponent<Bible>().PlayerTransform = GameManager.Instance.Player.transform;
        yield return new WaitForSeconds(AttackInterval);
        go.SetActive(false);

        while (true)
        {
            yield return new WaitUntil(() => IsGenerate == true);
            yield return new WaitForSeconds(AttackInterval);
            yield return new WaitUntil(() => IsGenerate == true);
            go.SetActive(true);
            yield return new WaitUntil(() => IsGenerate == true);
            yield return new WaitForSeconds(_activeTime);
            yield return new WaitUntil(() => IsGenerate == true);
            go.SetActive(false);
        }
    }

    public override void Move()
    {
        _rb2D.MovePosition(new Vector2(MOVE_VALUE * Mathf.Sin(Time.time * MOVE_VALUE) + GameManager.Instance.Player.transform.position.x, MOVE_VALUE * Mathf.Cos(Time.time * MOVE_VALUE) + GameManager.Instance.Player.transform.position.y));
    }

    public override void LevelUp(int level)
    {
        _activeTime += ADD_ACTIVE_TIME;
    }

    public override void ResetStatus()
    {
        _activeTime = FIRST_ACTIVE_TIME;
    }

    public override void Pause()
    {
        _isMove = false;
        IsGenerate = false;
    }

    public override void Restart()
    {
        _isMove = true;
        IsGenerate = true;
    }
}
