using UnityEngine;
using UnityEngine.UI;

/// <summary>プレイヤーを動かす </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("HPバー")] Slider _hpSlider = default;
    [SerializeField, Tooltip("経験値バー")] Slider _expSlider = default;
    [SerializeField, Tooltip("最大体力")] int _maxHp = 0;
    [SerializeField, Tooltip("移動速度")] float _moveSpeed = 0;
    [SerializeField, Tooltip("テスト　あと作り直す")] float[] _nextLvUpEXP = default;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    /// <summary>最大体力, 回復, アーマー, 移動速度, 威力, エリア, 速度, 持続時間, 量,　クールダウン, 運気,　成長, 強欲, 呪い, 磁石 初期武器の添え字</summary>
    int[] _characterStatusArray = new int[16];
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

    /// <summary>ダメージを受ける </summary>
    /// <param name="damage">被ダメージ</param>
    public void GetDamage(int damage)
    {
        _currentHP -= damage;
        _hpSlider.value = (float)_currentHP / (float)_maxHp;
    }

    /// <summary>経験値を受け取る </summary>
    /// <param name="addEXP">増える経験値</param>
    public void GetEXP(int addEXP)
    {
        _currentEXP += addEXP;
        _expSlider.value = (float)_currentEXP / _nextLvUpEXP[_nextLevelIndex];

        if (_currentEXP >= _nextLvUpEXP[_nextLevelIndex])
        {
            LevelUp();
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

    /// <summary>一定以上の経験値が貯まったらプレイヤーのレベルを上げる </summary>
    void LevelUp()
    {
        _nextLevelIndex++;
        _expSlider.value = 0;
        GameManager.Instance.WeaponManager.SetSelectWeapons();
    }
}

