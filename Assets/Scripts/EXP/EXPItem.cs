using UnityEngine;

/// <summary>EXPアイテム</summary>
public class EXPItem : MonoBehaviour
{
    [SerializeField, Header("獲得できる経験値量")] int _addEXPValue = 3; 

    /// <summary>獲得できる経験値量 </summary>
    public int AddEXPValue { get => _addEXPValue; }
}
