using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>�v���C���[�𓮂��� </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�����")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("HP�o�[")] Slider _hpSlider = default;
    [SerializeField, Tooltip("�o���l�o�[")] Slider _expSlider = default;
    [SerializeField, Tooltip("�ő�̗�")] int _maxHp = 0;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 0;
    /// <summary>�ő�̗�, ��, �A�[�}�[, �ړ����x, �З�, �G���A, ���x, ��������, ��,�@�N�[���_�E��, �^�C,�@����, ���~, ��, ���� ��������̓Y����</summary>
    int[] _characterStatusArray = new int[16];
    /// <summary>�e�X�g�@���ƍ�蒼�� </summary>
    [SerializeField] float[] _nextLvUpEXP = default;

    Rigidbody2D _rb2D = default;
    /// <summary>���ݏ������Ă���o���l </summary>
    int _currentEXP = 0;
    /// <summary>���݂̗̑� </summary>
    int _currentHP = 0;
    /// <summary>���݂̃��x�� </summary>
    int _currentLevel = 1;
    /// <summary>���̃��x���A�b�v�܂łɂ�����l�̔z��̓Y����</summary>
    int _nextLevelIndex = 0;
    /// <summary>�v���C���[������(�g�p)���Ă��镐��̓Y����</summary>
    int[] _selectedWeapons = new int[6];
    float _horizontal = 0f;
    float _vertical = 0f;

    public int[] CharacterStatusArray { get => _characterStatusArray; set => _characterStatusArray = value; }

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _expSlider = GameObject.Find("EXPBar").GetComponent<Slider>();
        _currentHP = _maxHp;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (int i in _characterStatusArray)
            {
                Debug.Log(i);
            }
        }
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
        _currentHP -= damage;
        _hpSlider.value = (float)_currentHP / (float)_maxHp;
    }

    /// <summary>�o���l���󂯎�� </summary>
    /// <param name="addEXP">������o���l</param>
    public void GetEXP(int addEXP)
    {
        _currentEXP += addEXP;
        _expSlider.value = (float)_currentEXP / _nextLvUpEXP[_nextLevelIndex];

        if (_currentEXP >= _nextLvUpEXP[_nextLevelIndex])
        {
            LevelUp();
        }
    }

    /// <summary>���ȏ�̌o���l�����܂�����v���C���[�̃��x�����グ�� </summary>
    void LevelUp()
    {
        _nextLevelIndex++;
        _expSlider.value = 0;
    }
}

