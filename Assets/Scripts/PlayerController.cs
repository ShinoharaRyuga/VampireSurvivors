using UnityEngine;
using UnityEngine.UI;

/// <summary>�v���C���[�𓮂��� </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�����")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("HP�o�[")] Slider _hpSlider = default;
    [SerializeField, Tooltip("�o���l�o�[")] Slider _expSlider = default;
    [SerializeField, Tooltip("�ő�̗�")] int _maxHp = 0;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 0;
    /// <summary>�e�X�g�@���ƍ�蒼�� </summary>
    [SerializeField] int[] _nextLvUPEXP = default;

    Rigidbody2D _rb2D = default;
    /// <summary>���ݏ������Ă���o���l </summary>
    int _currentEXP = 0;
    /// <summary>���݂̗̑� </summary>
    int _currentHP = 0;
    float _horizontal = 0f;
    float _vertical = 0f;
   
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(_weapons[1].Generator(this.transform));      //��������̐������J�n
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
        Debug.Log("($�E�E)/~~~");
    }

    public void GetEXP(int addEXP)
    {
        _currentEXP += addEXP;
    }
}
