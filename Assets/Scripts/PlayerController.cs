using UnityEngine;

/// <summary>�v���C���[�𓮂��� </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�����")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("�ړ����x")] int _moveSpeed = 0;
    [SerializeField] Knife Knife;
    Rigidbody2D _rb2D = default;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(_weapons[0].Generator(this.transform));
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
