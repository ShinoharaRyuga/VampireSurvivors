using UnityEngine;
using UnityEngine.UI;

/// <summary>プレイヤーを動かす </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("武器種")] WeaponBase[] _weapons = default;
    [SerializeField, Tooltip("HPバー")] Slider _hpSlider = default;
    [SerializeField, Tooltip("経験値バー")] Slider _expSlider = default;
    [SerializeField, Tooltip("最大体力")] int _maxHp = 0;
    [SerializeField, Tooltip("移動速度")] float _moveSpeed = 0;
    /// <summary>テスト　あと作り直す </summary>
    [SerializeField] int[] _nextLvUPEXP = default;

    Rigidbody2D _rb2D = default;
    /// <summary>現在所持している経験値 </summary>
    int _currentEXP = 0;
    /// <summary>現在の体力 </summary>
    int _currentHP = 0;
    float _horizontal = 0f;
    float _vertical = 0f;
   
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(_weapons[1].Generator(this.transform));      //初期武器の生成を開始
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

    /// <summary>ダメージを受ける </summary>
    /// <param name="damage">被ダメージ</param>
    public void GetDamage(int damage)
    {
        Debug.Log("($・・)/~~~");
    }

    public void GetEXP(int addEXP)
    {
        _currentEXP += addEXP;
    }
}
