using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�j���j�N�@ToDo.�͈͓��̓G�Ƀ_���[�W��^���� </summary>
public class Garlic : WeaponBase
{
    [SerializeField, Tooltip("�Փ˂����郌�C���[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("���a")] float _radius = 1f;
    [SerializeField, Tooltip("���ʎ���")] float _activeTime = 1f;
    /// <summary>�U���\�ȏ�Ԃł��邩�ǂ��� </summary>
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
