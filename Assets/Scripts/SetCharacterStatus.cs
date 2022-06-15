using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("最大体力, 回復, アーマー, 移動速度, 威力, エリア, 速度, 持続時間, 量,　クールダウン, 運気,　成長, 強欲, 呪い, 磁石 初期武器の添え字")]
    float[] _characterStatus = new float[16];

    public void SetStatus()
    {
        GameManager.Instance.SelectedCharacterStatus = _characterStatus;
    }
}
