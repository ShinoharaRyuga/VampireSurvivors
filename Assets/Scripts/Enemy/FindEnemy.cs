using System.Linq;
using UnityEngine;

/// <summary>���@�̏�≊�̏�Ɏg�� �����𖞂������G���擾����</summary>
public class FindEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("�Փ˂����郌�C���[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("���G�͈�")] float _radius = 1f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    /// <summary>�v���C���[����ł��߂��G���擾���� </summary>
    /// <returns>�G�̈ʒu</returns>
    public Vector3 GetMostNearEnemy()
    {
        var nearEnemies = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayerMask);

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
        var enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayerMask);
        if (enemies.Length == 0)    //�G���擾�o���Ȃ�������
        {
            return Vector3.zero;
        }
        var index = Random.Range(0, enemies.Length - 1);
        var nearEnemyCollider = enemies[index];
        return nearEnemyCollider.transform.position;
    }
}
