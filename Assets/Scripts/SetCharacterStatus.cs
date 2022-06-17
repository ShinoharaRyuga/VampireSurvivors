using TMPro;
using UnityEngine;

public class SetCharacterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("最大体力, 回復, アーマー, 移動速度, 威力, エリア, 速度, 持続時間, 量,　クールダウン, 運気,　成長, 強欲, 呪い, 磁石 初期武器の添え字")]
    float[] _characterStatus = new float[16];
    [SerializeField, Tooltip("初期武器を表示するテキスト")] TextMeshProUGUI _firstWeaponText = default;
    [SerializeField, Tooltip("使用武器の名前")] string _weaponName = "";

    public void SetStatus()
    {
        GameManager.Instance.SelectedCharacterStatus = _characterStatus;
        _firstWeaponText.text = $"使用武器:{_weaponName}";
    }
}
