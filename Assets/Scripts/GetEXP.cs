using UnityEngine;

/// <summary>範囲内にあるジェムを取得する </summary>
public class GetEXP : MonoBehaviour
{
    [SerializeField, Tooltip("衝突させるレイヤー")] LayerMask _expLayerMask = default;
    [SerializeField, Tooltip("取得範囲の半径")] float _radius = 1f;

    PlayerController _player => GetComponent<PlayerController>();
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius/* + GameManager.Instance.Player.CharacterStatusArray[14]*/);
    }

    private void Update()
    {
        var expobjects = Physics2D.OverlapCircleAll(transform.position, _radius + GameManager.Instance.Player.CharacterStatusArray[14], _expLayerMask);
      //  Debug.Log(GameManager.Instance.Player.CharacterStatusArray[14]);
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
