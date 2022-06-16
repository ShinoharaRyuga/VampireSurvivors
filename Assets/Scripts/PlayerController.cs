using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>プレイヤーを動かす </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("HPバー")] Slider _hpSlider = default;
    [SerializeField, Tooltip("上下左右のスプライト")] Sprite[] _playerSprites = default;
    [SerializeField, Tooltip("次に必要なEXP(増やす量)")] int _addNextEXP = 50;
    [SerializeField] SpriteRenderer _spriteRenderer = default;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    TextMeshProUGUI _levelText = default;
    Slider _expBar = default;
    /// <summary>最大体力, 回復, アーマー, 移動速度, 威力, エリア, 速度, 持続時間, 量, クールダウン, 運気,　成長, 強欲, 呪い, 磁石 初期武器の添え字</summary>
    float[] _characterStatusArray = new float[16];
    /// <summary>プレイヤーが所持(使用)している武器の添え字</summary>
    int[] _selectedWeapons = new int[6];
    int[] _selectedEffectWeapons = new int[6];
    /// <summary>現在所持している経験値 </summary>
    int _currentEXP = 0;
    /// <summary>現在の体力 </summary>
    int _currentHP = 0;
    /// <summary>現在のレベル </summary>
    int _currentLevel = 1;
    /// <summary>次のレベルアップまでにかかる値の配列の添え字</summary>
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

    /// <summary>ダメージを受ける </summary>
    /// <param name="damage">被ダメージ</param>
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

    /// <summary>経験値を受け取る </summary>
    /// <param name="addEXP">増える経験値</param>
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

    /// <summary>使用している武器を保存する </summary>
    /// <param name="index">武器配列の添え字</param>
    /// <param name="type">武器/効果武器どちらか</param>
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

    /// <summary>一定以上の経験値が貯まったらプレイヤーのレベルを上げる </summary>
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

