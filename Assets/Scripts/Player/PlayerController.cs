using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�v���C���[�𓮂��� </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class PlayerController : MonoBehaviour, IPause
{
    /// <summary>�v���C���[�X�e�[�^�X�̐� </summary>
    const int PLAYER_STATUS_INDEX = 6;
    /// <summary>���̃��x���A�b�v�܂ł̒l�̏����l</summary>
    const int FIRST_NEXT_LEVEL_VALUE = 10;

    /// <summary>0=���ʁ@1=���p 2=���E�̎p</summary>
    [SerializeField, Tooltip("�㉺���E�̃X�v���C�g")] Sprite[] _playerSprites = default;
    [SerializeField, Tooltip("HP�o�[")] Slider _hpSlider = default;
    [SerializeField, Tooltip("�o���l���擾����SE")] AudioClip _getSE = null;
    [SerializeField, Tooltip("�v���C���[�X�v���C�g")] SpriteRenderer _playerSprite = default;
    [SerializeField, Tooltip("���ɕK�v��EXP(���₷��)")] int _addNextEXP = 50;

    /// <summary>���x����\������e�L�X�g</summary>
    TextMeshProUGUI _levelText = default;
    /// <summary>�o���l�ʂ�\������X���C�_�[</summary>
    Slider _expBar = default;
    /// <summary>0=�ő�̗́@1=�񕜁@2=�ړ����x�@3=���΁@4=�ʁ@5=��������</summary>
    float[] _playerStatus = new float[PLAYER_STATUS_INDEX] {30, 0, 4, 0, 0, 0 };
    /// <summary>���ݏ������Ă���o���l </summary>
    int _currentEXP = 0;
    /// <summary>���݂̗̑� </summary>
    int _currentHP = 0;
    /// <summary>���݂̃��x�� </summary>
    int _currentLevel = 1;
    /// <summary>���̃��x���A�b�v�܂łɂ�����l</summary>
    int _nextLevelEXP = FIRST_NEXT_LEVEL_VALUE;
    /// <summary>���͒l(����)</summary>
    float _horizontal = 0f;
    /// <summary>���͒l(����) </summary>
    float _vertical = 0f;
    /// <summary>�ړ��\���ǂ��� </summary>
    bool _isMove = true;

    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    AudioSource _audioSource => GetComponent<AudioSource>();

    /// <summary>0=�ő�̗́@1=�񕜁@2=�ړ����x�@3=���΁@4=�ʁ@5=��������</summary>
    public float[] CharacterStatusArray { get => _playerStatus; }

    /// <summary>���݂̃��x�� </summary>
    public int CurrentLevel { get => _currentLevel; }

    void Start()
    {
        _expBar = GameObject.Find("EXPBar").GetComponent<Slider>();
        _levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        GameManager.Instance.AddPauseObject(this);
        _levelText.text = $"Lv.{_currentLevel}";
        _currentHP = (int)_playerStatus[0];
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (_isMove)    //�ړ��\�ł���΃C���X�g��؂�ւ���
        {
            ChangeSprite();
        }
    }

    void FixedUpdate()
    {
        //�ړ�����
        if (_isMove)    
        {
            Vector2 dir = new Vector2(_horizontal, _vertical).normalized * _playerStatus[2];
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
        _currentHP -= damage;
        _hpSlider.value = (float)_currentHP / _playerStatus[0];

        if (_currentHP <= 0)�@//���S
        {
            GameManager.Instance.GameOver();
        }
    }

    /// <summary>�o���l���󂯎�� </summary>
    /// <param name="addEXP">�擾�o���l��</param>
    public void GetEXP(int addEXP)
    {
        _currentEXP += addEXP;
        _expBar.value = (float)_currentEXP / _nextLevelEXP;
        _audioSource.PlayOneShot(_getSE);
        if (_currentEXP >= _nextLevelEXP)
        {
            LevelUp();
        }
    }

    /// <summary>�v���C���[�̗̑͂��񕜂����� </summary>
    /// <param name="addValue">�񕜗�</param>
    public void Heel(int addValue)
    {
        if (_currentHP < _playerStatus[0])
        {
            _currentHP += addValue;
            _hpSlider.value = (float)_currentHP / _playerStatus[0];
        }
    }

    /// <summary>���͕����ɉ����ăX�v���C�g��ύX����</summary>
    public void ChangeSprite()
    {
        if (Input.GetKeyDown(KeyCode.W))�@//���ɐi��
        {
            _playerSprite.sprite = _playerSprites[1];
        }
        else if (Input.GetKeyDown(KeyCode.S))�@//�O�ɐi��
        {
            _playerSprite.sprite = _playerSprites[0];
        }
        else if (Input.GetKeyDown(KeyCode.A))�@//���ɐi��
        {
            _playerSprite.sprite = _playerSprites[2];
            _playerSprite.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))�@//�E�ɐi��
        {
            _playerSprite.sprite = _playerSprites[2];
            _playerSprite.flipX = true;
        }
    }

    /// <summary>���ȏ�̌o���l�����܂�����v���C���[�̃��x�����グ�� </summary>
    void LevelUp()
    {
        _nextLevelEXP += _addNextEXP * _currentLevel;�@//���̃��x���A�b�v�܂ł̌o���l�ʂ��v�Z���������킹��
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

