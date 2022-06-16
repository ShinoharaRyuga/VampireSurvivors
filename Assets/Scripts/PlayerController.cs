using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�v���C���[�𓮂��� </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("HP�o�[")] Slider _hpSlider = default;
    [SerializeField, Tooltip("�㉺���E�̃X�v���C�g")] Sprite[] _playerSprites = default;
    [SerializeField, Tooltip("���ɕK�v��EXP(���₷��)")] int _addNextEXP = 50;
    [SerializeField] SpriteRenderer _spriteRenderer = default;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    TextMeshProUGUI _levelText = default;
    Slider _expBar = default;
    /// <summary>�ő�̗�, ��, �A�[�}�[, �ړ����x, �З�, �G���A, ���x, ��������, ��, �N�[���_�E��, �^�C,�@����, ���~, ��, ���� ��������̓Y����</summary>
    float[] _characterStatusArray = new float[16];
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
    int _nextLevelEXP = 30;
    int _weaponCount = 0;
    int _effectWeaponCount = 0;
    float _horizontal = 0f;
    float _vertical = 0f;
    bool _isMove = true;

    public float[] CharacterStatusArray { get => _characterStatusArray; set => _characterStatusArray = value; }
    public int CurrentLevel { get => _currentLevel; }

    void Start()
    {
        _expBar = GameObject.Find("EXPBar").GetComponent<Slider>();
        _levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        GameManager.Instance.AddPauseObject(this);
        _levelText.text = $"Lv.{_currentLevel}";
        _currentHP = (int)_characterStatusArray[0];
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.J))
        {
            Array.ForEach(_characterStatusArray, s => Debug.Log(s));
        }

        ChangeImage();
    }

    void FixedUpdate()
    {
        if (_isMove)
        {
            Vector2 dir = new Vector2(_horizontal, _vertical).normalized * _characterStatusArray[3];
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
        else
        {
            _rb2D.velocity = Vector2.zero;
        }
    }

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damage">��_���[�W</param>
    public void GetDamage(int damage)
    {
        //damage -= (int)_characterStatusArray[2];

        //if (damage < 0)
        //{
        //    damage = 1;
          
        //}
      
        _currentHP -= damage;
        _hpSlider.value = (float)_currentHP / _characterStatusArray[0];

        if (_currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    /// <summary>�o���l���󂯎�� </summary>
    /// <param name="addEXP">������o���l</param>
    public void GetEXP(int addEXP)
    {
        _currentEXP += addEXP;
        _expBar.value = (float)_currentEXP / _nextLevelEXP;

        if (_currentEXP >= _nextLevelEXP)
        {
            LevelUp();
        }
    }

    public void Heel(int addValue)
    {
        if (_currentHP < _characterStatusArray[0])
        {
            _currentHP += addValue;
            _hpSlider.value = (float)_currentHP / _characterStatusArray[0];
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

    public void ChangeImage()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _spriteRenderer.sprite = _playerSprites[1];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _spriteRenderer.sprite = _playerSprites[0];
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _spriteRenderer.sprite = _playerSprites[2];
            _spriteRenderer.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _spriteRenderer.sprite = _playerSprites[2];
            _spriteRenderer.flipX = true;
        }
    }

    /// <summary>���ȏ�̌o���l�����܂�����v���C���[�̃��x�����グ�� </summary>
    void LevelUp()
    {
        _nextLevelEXP += _addNextEXP * _currentLevel;
        _currentLevel++;
        _levelText.text = $"Lv.{_currentLevel}";
        _expBar.value = 0;
        GameManager.Instance.WeaponManager.SetSelectWeapons();
        GameManager.Instance.Pause();
    }

    public void Pause()
    {
        _isMove = false;
    }

    public void Restart()
    {
       
        _isMove = true;
    }
}

