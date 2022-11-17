using UnityEngine;

/// <summary>�o�ߎ��ԂŃv���C���[�̗̑͂��񕜂��镐��̃N���X </summary>
public class Heal : EffectWeaponBase
{
    [SerializeField, Tooltip("�񕜗�")] float _addHeelPoint = 0.1f;
    /// <summary>�񕜂��Ă��悢�� </summary>
    bool _isActive = false;
    float _tmpHealPoint = 0f;
    float _time = 0f;

    /// <summary>�񕜂��Ă��悢�� </summary>
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

            if (1f <= _time)�@//�������񕜂���
            {
                _tmpHealPoint += (_addHeelPoint + GameManager.Instance.Player.PlayerStatus[1]);
                _time = 0f;
            }

            if (1f <= _tmpHealPoint)    //�񕜗ʂ�1�𒴂�����񕜂���
            {
                GameManager.Instance.Player.Heel((int)_tmpHealPoint);
                _tmpHealPoint = 0f;
            };
        }
    }
}
