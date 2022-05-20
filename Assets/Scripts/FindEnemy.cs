using System.Linq;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("衝突させるレイヤー")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("検索の中心")] Vector2 _findCenter = default;
    [SerializeField, Tooltip("検索の半径")] float _radius = 1f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetCenter(), _radius);
    }

    Vector3 GetCenter()
    {
        var center = transform.position + transform.up * _findCenter.y + transform.right * _findCenter.x;
        return center;
    }

    /// <summary>プレイヤーから最も近い敵を取得する </summary>
    /// <returns>敵の位置</returns>
    public Vector3 GetMostNearEnemy()
    {
        var nearEnemies = Physics2D.OverlapCircleAll(GetCenter(), _radius, _targetLayerMask);
        var nearEnemyCollider = nearEnemies.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).FirstOrDefault();
        return nearEnemyCollider.transform.position;
    }
}
