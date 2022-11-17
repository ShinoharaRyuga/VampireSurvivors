using UnityEngine;

/// <summary>経過時間でプレイヤーの体力が回復する武器のクラス </summary>
public class Heal : EffectWeaponBase
{
    [SerializeField, Tooltip("回復量")] float _addHeelPoint = 0.1f;
    /// <summary>回復してもよいか </summary>
    bool _isActive = false;
    float _tmpHealPoint = 0f;
    float _time = 0f;

    /// <summary>回復してもよいか </summary>
    public bool IsActive { get => _isActive; set => _isActive = value; }

    public override void Effect()
    {
        var go = Instantiate(this);
        go.IsActive = true;
    }

    void Update()
    {
        if (_isActive)
        {
            _time += Time.deltaTime;

            if (1f <= _time)　//少しずつ回復する
            {
                _tmpHealPoint += (_addHeelPoint + GameManager.Instance.Player.PlayerStatus[1]);
                _time = 0f;
            }

            if (1f <= _tmpHealPoint)    //回復量が1を超えたら回復する
            {
                GameManager.Instance.Player.Heel((int)_tmpHealPoint);
                _tmpHealPoint = 0f;
            };
        }
    }
}
