using UnityEngine;

/// <summary>EXPACe</summary>
public class EXPItem : MonoBehaviour
{
    [SerializeField, Header("l¾Å«éo±lÊ")] int _addEXPValue = 3; 

    /// <summary>l¾Å«éo±lÊ </summary>
    public int AddEXPValue { get => _addEXPValue; }
}
