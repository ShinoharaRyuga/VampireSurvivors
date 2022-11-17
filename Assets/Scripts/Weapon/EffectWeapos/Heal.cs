using UnityEngine;

public class Heal : EffectWeaponBase
{
    [SerializeField, Tooltip("‰ñ•œ—Ê")] float _addHeelPoint = 0.1f;
    bool _isActive = false;
    float _tmpHeelPoint = 0f;
    float _time = 0f;

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
            if (1f <= _time)
            {
                _tmpHeelPoint += (_addHeelPoint + GameManager.Instance.Player.CharacterStatusArray[1]);
                _time = 0f;
            }

            if (1f <= _tmpHeelPoint)
            {
                GameManager.Instance.Player.Heel(1);
                _tmpHeelPoint = 0f;
            };
        }
    }
}
