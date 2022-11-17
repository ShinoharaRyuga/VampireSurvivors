using UnityEngine;

/// <summary>範囲内にあるジェムを取得する </summary>
[RequireComponent(typeof(PlayerController))]
public class GetEXPItem : MonoBehaviour
{
    [SerializeField, Tooltip("衝突させるレイヤー")] LayerMask _expLayerMask = default;
    [SerializeField, Tooltip("取得範囲の半径")] float _radius = 1f;

    PlayerController _player => GetComponent<PlayerController>();
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Update()
    {
        //EXPアイテムを取得する
        var expobjects = Physics2D.OverlapCircleAll(transform.position, _radius + GameManager.Instance.Player.PlayerStatus[3], _expLayerMask); 

        if (0 < expobjects.Length)
        {
            foreach (var go in expobjects)
            {
                _player.GetEXP(go.GetComponent<EXPItem>().AddEXPValue);
                go.gameObject.SetActive(false);
            }
        }
    }

}
