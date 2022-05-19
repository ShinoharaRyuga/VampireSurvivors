using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField, Tooltip("�̗�")] int _hp = 1;
    [SerializeField, Tooltip("�U����")] int _attackPower = 1;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 1f;
    [SerializeField, Tooltip("���S���ɗ��Ƃ��o���l�̒l")] int _dropEXPValue = 1;
    [SerializeField, Tooltip("�v���C���[ �U���Ώ�")] GameObject _player = default;
    [SerializeField, Tooltip("EXP�I�u�W�F�N�g ������")] SetEXP _expObj = default;

    Rigidbody2D _rb2D = default;
    PlayerController _playerController => _player.GetComponent<PlayerController>();
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var dir = (_player.transform.position - transform.position).normalized * _moveSpeed;
        _rb2D.velocity = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.GetDamage(_attackPower);
        }
    }

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damage">��_���[�W</param>
    public void GetDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            var go = Instantiate(_expObj, transform.position, Quaternion.identity);
            go.AddEXP = _dropEXPValue;
            Destroy(gameObject);
        }
    }
}
