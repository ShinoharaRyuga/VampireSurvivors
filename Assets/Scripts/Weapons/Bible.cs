using System.Collections;
using UnityEngine;

public class Bible : WeaponBase
{
    Transform _playerTransform = default;
    Rigidbody2D _rb2D = default;

    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public Rigidbody2D Rb2D { get => _rb2D; set => _rb2D = value; }

    private void Update()
    {
        Move(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, false);
    }

    public override IEnumerator Generator(Transform playerTransform)
    {
        var go = Instantiate(gameObject, new Vector2(playerTransform.position.x + 3, playerTransform.position.y), Quaternion.identity);
        go.GetComponent<Bible>().Rb2D = go.GetComponent<Rigidbody2D>();
        go.GetComponent<Bible>().PlayerTransform = playerTransform;
        yield return new WaitForSeconds(2);
        go.SetActive(false);

        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            go.SetActive(true);
            yield return new WaitForSeconds(2);
            go.SetActive(false);
        }
    }

    public override void Move(Transform playerTransform)
    {
        _rb2D.MovePosition(new Vector2(3 * Mathf.Sin(Time.time * 3) + _playerTransform.position.x, 3 * Mathf.Cos(Time.time * 3) + _playerTransform.position.y));
    }
}
