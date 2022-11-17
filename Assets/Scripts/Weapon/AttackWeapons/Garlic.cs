using System;
using System.Collections;
using UnityEngine;

/// <summary>ニンニク　範囲内の敵にダメージを与える </summary>
public class Garlic : WeaponBase, IPause
{
    [SerializeField, Tooltip("衝突させるレイヤー")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("半径")] float _radius = 1f;
    [SerializeField, Tooltip("効果時間")] float _activeTime = 1f;
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    /// <summary>攻撃可能な状態であるかどうか </summary>
    bool _isActive = false;
    bool _isPause = true;

    private void Start()
    {
        GameManager.Instance.AddPauseObject(this);
        transform.parent = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        if (_isActive && _isPause)
        {
            var enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayerMask);

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().GetDamage(1);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public override IEnumerator Generator()
    {
        while (true)
        {
            _isActive = true;
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(_activeTime);
            yield return new WaitUntil(() => _isPause == true);
            _isActive = false;
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(AttackInterval);
            yield return new WaitUntil(() => _isPause == true);
        }
    }

    public override void LevelUp(int level)
    {
        _activeTime += 0.2f;
    }

    public override void ResetStatus()
    {
        _activeTime += 1;
    }

    public void Pause()
    {
       _isPause = false;
    }

    public void Restart()
    {
        _isPause = true;
    }

    public override void Move()
    {
        
    }
}
