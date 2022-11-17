using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>プレイヤーを動かす </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class PlayerController : MonoBehaviour, IPause
{
    /// <summary>プレイヤーステータスの数 </summary>
    const int PLAYER_STATUS_INDEX = 6;
    /// <summary>次のレベルアップまでの値の初期値</summary>
    const int FIRST_NEXT_LEVEL_VALUE = 10;

    /// <summary>0=正面　1=後ろ姿 2=左右の姿</summary>
    [SerializeField, Tooltip("上下左右のスプライト")] Sprite[] _playerSprites = default;
    [SerializeField, Tooltip("HPバー")] Slider _hpSlider = default;
    [SerializeField, Tooltip("経験値を取得時のSE")] AudioClip _getSE = null;
    [SerializeField, Tooltip("プレイヤースプライト")] SpriteRenderer _playerSprite = default;
    [SerializeField, Tooltip("次に必要なEXP(増やす量)")] int _addNextEXP = 50;

    /// <summary>レベルを表示するテキスト</summary>
    TextMeshProUGUI _levelText = default;
    /// <summary>経験値量を表示するスライダー</summary>
    Slider _expBar = default;
    /// <summary>0=最大体力　1=回復　2=移動速度　3=磁石　4=量　5=初期武器</summary>
    float[] _playerStatus = new float[PLAYER_STATUS_INDEX] {30, 0, 4, 0, 0, 0 };
    /// <summary>現在所持している経験値 </summary>
    int _currentEXP = 0;
    /// <summary>現在の体力 </summary>
    int _currentHP = 0;
    /// <summary>現在のレベル </summary>
    int _currentLevel = 1;
    /// <summary>次のレベルアップまでにかかる値</summary>
    int _nextLevelEXP = FIRST_NEXT_LEVEL_VALUE;
    /// <summary>入力値(水平)</summary>
    float _horizontal = 0f;
    /// <summary>入力値(垂直) </summary>
    float _vertical = 0f;
    /// <summary>移動可能かどうか </summary>
    bool _isMove = true;

    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    AudioSource _audioSource => GetComponent<AudioSource>();

    /// <summary>0=最大体力　1=回復　2=移動速度　3=磁石　4=量　5=初期武器</summary>
    public float[] CharacterStatusArray { get => _playerStatus; }

    /// <summary>現在のレベル </summary>
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

        if (_isMove)    //移動可能であればイラストを切り替える
        {
            ChangeSprite();
        }
    }

    void FixedUpdate()
    {
        //移動処理
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

    /// <summary>ダメージを受ける </summary>
    /// <param name="damage">被ダメージ</param>
    public void GetDamage(int damage)
    {
        _currentHP -= damage;
        _hpSlider.value = (float)_currentHP / _playerStatus[0];

        if (_currentHP <= 0)　//死亡
        {
            GameManager.Instance.GameOver();
        }
    }

    /// <summary>経験値を受け取る </summary>
    /// <param name="addEXP">取得経験値量</param>
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

    /// <summary>プレイヤーの体力を回復させる </summary>
    /// <param name="addValue">回復量</param>
    public void Heel(int addValue)
    {
        if (_currentHP < _playerStatus[0])
        {
            _currentHP += addValue;
            _hpSlider.value = (float)_currentHP / _playerStatus[0];
        }
    }

    /// <summary>入力方向に応じてスプライトを変更する</summary>
    public void ChangeSprite()
    {
        if (Input.GetKeyDown(KeyCode.W))　//後ろに進む
        {
            _playerSprite.sprite = _playerSprites[1];
        }
        else if (Input.GetKeyDown(KeyCode.S))　//前に進む
        {
            _playerSprite.sprite = _playerSprites[0];
        }
        else if (Input.GetKeyDown(KeyCode.A))　//左に進む
        {
            _playerSprite.sprite = _playerSprites[2];
            _playerSprite.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))　//右に進む
        {
            _playerSprite.sprite = _playerSprites[2];
            _playerSprite.flipX = true;
        }
    }

    /// <summary>一定以上の経験値が貯まったらプレイヤーのレベルを上げる </summary>
    void LevelUp()
    {
        _nextLevelEXP += _addNextEXP * _currentLevel;　//次のレベルアップまでの経験値量を計算し足し合わせる
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

