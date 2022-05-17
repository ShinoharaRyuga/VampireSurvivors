using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("•Šíí")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("ˆÚ“®‘¬“x")] int _moveSpeed = 0;
    Rigidbody2D _rb2D = default;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(_weapons[0].Generator(transform.up));
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(h, v) * _moveSpeed;
        if (dir != Vector2.zero)
        {
            transform.up = dir;
            _rb2D.velocity = dir;
        }
    }
}
