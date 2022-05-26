using UnityEngine;

/// <summary>�v���C���[���W�F�����擾�������ɒǉ������o���l�ʂ�ݒ肷��</summary>
public class SetEXP : MonoBehaviour
{
    [SerializeField] int _testPoint = 0;
    /// <summary>�ǉ�����o���l�� </summary>
    int _addEXP = 0;
    /// <summary>�ǉ�����o���l�� </summary>
    public int AddEXP
    {
        get { return _addEXP; }
        set
        {
            if (value <= 0)
            {
                _addEXP = 1;
            }
            else
            {
                _addEXP = value;
            }
        }
    }

    public int TestPoint { get => _testPoint; set => _testPoint = value; }
}
