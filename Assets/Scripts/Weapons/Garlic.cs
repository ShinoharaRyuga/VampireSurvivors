using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ニンニク　ToDo.範囲内の敵にダメージを与える </summary>
public class Garlic : WeaponBase
{
    [SerializeField, Tooltip("衝突させるレイヤー")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("半径")] float _radius = 1f;
    [SerializeField, Tooltip("効果時間")] float _activeTime = 1f;
    /// <summary>攻撃可能な状態であるかどうか </summary>
    bool _isActive = false;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (_isActive)
        {
            var enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayerMask);

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyStatus>().GetDamage(1);
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
           // Debug.Log(_isActive);
            yield return new WaitForSeconds(_activeTime);
            _isActive = false;
           // Debug.Log(_isActive);
            yield return new WaitForSeconds(AttackInterval);
        }
    }

    public override void Move()
    {
        
    }
}
