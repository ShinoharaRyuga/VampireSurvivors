using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�j���j�N�@ToDo.�͈͓��̓G�Ƀ_���[�W��^���� </summary>
public class Garlic : WeaponBase, IPause
{
    [SerializeField, Tooltip("�Փ˂����郌�C���[")] LayerMask _targetLayerMask = default;
    [SerializeField, Tooltip("���a")] float _radius = 1f;
    [SerializeField, Tooltip("���ʎ���")] float _activeTime = 1f;
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    /// <summary>�U���\�ȏ�Ԃł��邩�ǂ��� </summary>
    bool _isActive = false;
    bool _isPause = true;

    private void Start()
    {
        GameManager.Instance.AddPauseObject(this);
        transform.parent = GameManager.Instance.Player.transform;
        Debug.Log("start");
    }

    private void Update()
    {
        if (_isActive && _isPause)
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
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(_activeTime);
            yield return new WaitUntil(() => _isPause == true);
            _isActive = false;
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(AttackInterval);
            yield return new WaitUntil(() => _isPause == true);
        }
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
