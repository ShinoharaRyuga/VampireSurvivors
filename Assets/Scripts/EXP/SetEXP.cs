using UnityEngine;

/// <summary>プレイヤーがジェムを取得した時に追加される経験値量を設定する</summary>
public class SetEXP : MonoBehaviour
{
    [SerializeField] int _testPoint = 0;
    /// <summary>追加する経験値量 </summary>
    int _addEXP = 0;
    /// <summary>追加する経験値量 </summary>
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
