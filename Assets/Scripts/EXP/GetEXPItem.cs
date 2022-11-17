using UnityEngine;

/// <summary>�͈͓��ɂ���W�F�����擾���� </summary>
[RequireComponent(typeof(PlayerController))]
public class GetEXPItem : MonoBehaviour
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
        //EXP�A�C�e�����擾����
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
