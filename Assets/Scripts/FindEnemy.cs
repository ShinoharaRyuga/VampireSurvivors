using System.Linq;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("�Փ˂����郌�C���[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("���G�̒��S")] Vector2 _findCenter = default;
    [SerializeField, Tooltip("���G")] float _radius = 1f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetCenter(), _radius);
    }

    /// <summary>���G�̒��S��Ԃ�</summary>
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

        if (nearEnemies.Length == 0)    //�G���擾�o���Ȃ�������
        {
            return Vector3.zero;
        }
        var nearEnemyCollider = nearEnemies.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).FirstOrDefault();
        return nearEnemyCollider.transform.position;
    }

    /// <summary>�v���C���[������͈͓̔��ɂ���G�̈ʒu���擾���� </summary>
    /// <returns>�G�̈ʒu</returns>
    public Vector3 GetRandomEnemy()
    {
        var Enemies = Physics2D.OverlapCircleAll(GetCenter(), _radius, _targetLayerMask);
        if (Enemies.Length == 0)    //�G���擾�o���Ȃ�������
        {
            return Vector3.zero;
        }
        var index = Random.Range(0, Enemies.Length - 1);
        var nearEnemyCollider = Enemies[index];
        return nearEnemyCollider.transform.position;
    }
}
