using UnityEngine;

/// <summary>EXP�A�C�e��</summary>
public class EXPItem : MonoBehaviour
{
    [SerializeField, Header("�l���ł���o���l��")] int _addEXPValue = 3; 

    /// <summary>�l���ł���o���l�� </summary>
    public int AddEXPValue { get => _addEXPValue; }
}
