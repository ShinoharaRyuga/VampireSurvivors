using TMPro;
using UnityEngine;

public class SetCharacterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("最大体力　回復　移動速度　磁石　量　初期武器")] float[] _characterStatus = new float[6];
    [SerializeField, Tooltip("初期武器を表示するテキスト")] TextMeshProUGUI _firstWeaponText = default;
    [SerializeField, Tooltip("使用武器の名前")] string _weaponName = "";

    public void SetStatus()
    {
        GameManager.Instance.SelectedCharacterStatus = _characterStatus;
        _firstWeaponText.text = $"使用武器:{_weaponName}";
    }
}
