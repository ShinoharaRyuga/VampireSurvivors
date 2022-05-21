using System.Linq;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("Õ“Ë‚³‚¹‚éƒŒƒCƒ„[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("õ“G‚Ì’†S")] Vector2 _findCenter = default;
    [SerializeField, Tooltip("õ“G")] float _radius = 1f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetCenter(), _radius);
    }

    /// <summary>õ“G‚Ì’†S‚ğ•Ô‚·</summary>
    Vector3 GetCenter()
    {
        var center = transform.position + transform.up * _findCenter.y + transform.right * _findCenter.x;
        return center;
    }

    /// <summary>ƒvƒŒƒCƒ„[‚©‚çÅ‚à‹ß‚¢“G‚ğæ“¾‚·‚é </summary>
    /// <returns>“G‚ÌˆÊ’u</returns>
    public Vector3 GetMostNearEnemy()
    {
        var nearEnemies = Physics2D.OverlapCircleAll(GetCenter(), _radius, _targetLayerMask);

        if (nearEnemies.Length == 0)    //“G‚ğæ“¾o—ˆ‚È‚©‚Á‚½‚ç
        {
            return Vector3.zero;
        }
        var nearEnemyCollider = nearEnemies.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).FirstOrDefault();
        return nearEnemyCollider.transform.position;
    }
}
