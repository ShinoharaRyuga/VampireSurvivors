using UnityEngine;
using UnityEngine.UI;

/// <summary>�v���C���[�𓮂��� </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("HP�o�[")] Slider _hpSlider = default;
    [SerializeField, Tooltip("�o���l�o�[")] Slider _expSlider = default;
    [SerializeField, Tooltip("�ő�̗�")] int _maxHp = 0;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 0;
    [SerializeField, Tooltip("�e�X�g�@���ƍ�蒼��")] float[] _nextLvUpEXP = default;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    /// <summary>�ő�̗�, ��, �A�[�}�[, �ړ����x, �З�, �G���A, ���x, ��������, ��,�@�N�[���_�E��, �^�C,�@����, ���~, ��, ���� ��������̓Y����</summary>
    int[] _characterStatusArray = new int[16];
    /// <summary>�v���C���[������(�g�p)���Ă��镐��̓Y����</summary>
    int[] _selectedWeapons = new int[6];
    int[] _selectedEffectWeapons = new int[6];
    /// <summary>���ݏ������Ă���o���l </summary>
    int _currentEXP = 0;
    /// <summary>���݂̗̑� </summary>
    int _currentHP = 0;
    /// <summary>���݂̃��x�� </summary>
    int _currentLevel = 1;
    /// <summary>���̃��x���A�b�v�܂łɂ�����l�̔z��̓Y����</summary>
    int _nextLevelIndex = 0;
    int _weaponCount = 0;
    int _effectWeaponCount = 0;
    float _horizontal = 0f;
    float _vertical = 0f;

    public int[] CharacterStatusArray { get => _characterStatusArray; set => _characterStatusArray = value; }
    public int MaxHp { get => _maxHp; set => _maxHp = value; }

    void Start()
    {
        _expSlider = GameObject.Find("EXPBar").GetComponent<Slider>();
        _currentHP = _maxHp;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(_characterStatusArray[0]);
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

    /// <summary>�g�p���Ă��镐���ۑ����� </summary>
    /// <param name="index">����z��̓Y����</param>
    /// <param name="type">����/���ʕ���ǂ��炩</param>
    public void SetWeaponIndex(int index, WeaponType type)
    {
        if (type == WeaponType.Weapon)
        {
            _selectedWeapons[_weaponCount] = index;
            _weaponCount++;
        }
        else if (type == WeaponType.EffectWeapon)
        {
            _selectedEffectWeapons[_effectWeaponCount] = index;
            _effectWeaponCount++;
        }
    }

    /// <summary>���ȏ�̌o���l�����܂�����v���C���[�̃��x�����グ�� </summary>
    void LevelUp()
    {
        _nextLevelIndex++;
        _expSlider.value = 0;
        GameManager.Instance.WeaponManager.SetSelectWeapons();
    }
}

