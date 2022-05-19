using UnityEngine;

/// <summary>�v���C���[�𓮂��� </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�����")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 0;
    Rigidbody2D _rb2D = default;

    /// <summary>���ݏ������Ă���o���l </summary>
    int _currentEXP = 0;
    float _horizontal = 0f;
    float _vertical = 0f;
    /// <summary>���ݏ������Ă���o���l </summary>
    public int CurrentEXP { get => _currentEXP; set => _currentEXP = value; }

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(_weapons[0].Generator(this.transform));      //��������̐������J�n
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 dir = new Vector2(_horizontal, _vertical).normalized * _moveSpeed;
        if (dir != Vector2.zero)
        {
            transform.up = dir;
            _rb2D.velocity = dir;
        }
        else
        {
            _rb2D.velocity = Vector2.zero;
        }
    }

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damage">��_���[�W</param>
    public void GetDamage(int damage)
    {
        Debug.Log("�_���[�W���󂯂�");
    }
}
