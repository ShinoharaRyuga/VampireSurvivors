using System.Collections;
using UnityEngine;

public class Bible : WeaponBase, IPause
{
    bool _isMove = true;
    Transform _playerTransform = default;
    Rigidbody2D _rb2D = default;
    Bible _cloneBible = default;

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
        var go = Instantiate(gameObject, new Vector2(GameManager.Instance.Player.transform.position.x + 3, GameManager.Instance.Player.transform.position.y), Quaternion.identity);
        go.GetComponent<Bible>().Rb2D = go.GetComponent<Rigidbody2D>();
        go.GetComponent<Bible>().PlayerTransform = GameManager.Instance.Player.transform;
        yield return new WaitForSeconds(2);
        go.SetActive(false);

        while (true)
        {
            yield return new WaitUntil(() => IsGenerate == true);
            yield return new WaitForSeconds(AttackInterval);
            yield return new WaitUntil(() => IsGenerate == true);
            go.SetActive(true);
            yield return new WaitUntil(() => IsGenerate == true);
            yield return new WaitForSeconds(2);
            yield return new WaitUntil(() => IsGenerate == true);
            go.SetActive(false);
        }
    }

    public override void Move()
    {
        _rb2D.MovePosition(new Vector2(3 * Mathf.Sin(Time.time * 3) + GameManager.Instance.Player.transform.position.x, 3 * Mathf.Cos(Time.time * 3) + GameManager.Instance.Player.transform.position.y));
    }

    public void Pause()
    {
        _isMove = false;
        IsGenerate = false;
    }

    public void Restart()
    {
        _isMove = true;
        IsGenerate = true;
    }
}
