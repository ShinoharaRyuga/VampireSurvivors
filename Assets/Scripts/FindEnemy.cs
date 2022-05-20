using System.Linq;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("�Փ˂����郌�C���[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("�����̒��S")] Vector2 _findCenter = default;
    [SerializeField, Tooltip("�����̔��a")] float _radius = 1f;

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

    /// <summary>�v���C���[����ł��߂��G���擾���� </summary>
    /// <returns>�G�̈ʒu</returns>
    public Vector3 GetMostNearEnemy()
    {
        var nearEnemies = Physics2D.OverlapCircleAll(GetCenter(), _radius, _targetLayerMask);
        var nearEnemyCollider = nearEnemies.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).FirstOrDefault();
        return nearEnemyCollider.transform.position;
    }
}
