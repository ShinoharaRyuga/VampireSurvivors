using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>プレイヤーを動かす </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("武器種")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("HPバー")] Slider _hpSlider = default;
    [SerializeField, Tooltip("経験値バー")] Slider _expSlider = default;
    [SerializeField, Tooltip("最大体力")] int _maxHp = 0;
    [SerializeField, Tooltip("移動速度")] float _moveSpeed = 0;
    /// <summary>最大体力, 回復, アーマー, 移動速度, 威力, エリア, 速度, 持続時間, 量,　クールダウン, 運気,　成長, 強欲, 呪い, 磁石 初期武器の添え字</summary>
    int[] _characterStatusArray = new int[16];
    /// <summary>テスト　あと作り直す </summary>
    [SerializeField] float[] _nextLvUpEXP = default;

    Rigidbody2D _rb2D = default;
    /// <summary>現在所持している経験値 </summary>
    int _currentEXP = 0;
    /// <summary>現在の体力 </summary>
    int _currentHP = 0;
    /// <summary>現在のレベル </summary>
    int _currentLevel = 1;
    /// <summary>次のレベルアップまでにかかる値の配列の添え字</summary>
    int _nextLevelIndex = 0;
    /// <summary>プレイヤーが所持(使用)している武器の添え字</summary>
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

    /// <summary>一定以上の経験値が貯まったらプレイヤーのレベルを上げる </summary>
    void LevelUp()
    {
        _nextLevelIndex++;
        _expSlider.value = 0;
    }
}

