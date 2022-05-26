using UnityEngine;

/// <summary>�͈͓��ɂ���W�F�����擾���� </summary>
public class GetEXP : MonoBehaviour
{
    [SerializeField, Tooltip("�Փ˂����郌�C���[")] LayerMask _expLayerMask = default;
    [SerializeField, Tooltip("�擾�͈͂̔��a")] float _radius = 1f;

    PlayerController _player => GetComponent<PlayerController>();
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Update()
    {
        var expobjects = Physics2D.OverlapCircleAll(transform.position, _radius, _expLayerMask);

        if (0 < expobjects.Length)
        {
            foreach (var go in expobjects)
            {
                _player.GetEXP(go.GetComponent<SetEXP>().TestPoint);
                go.gameObject.SetActive(false);
            }
        }
    }

}
