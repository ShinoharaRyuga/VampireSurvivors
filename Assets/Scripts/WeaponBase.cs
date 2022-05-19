using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>����̊��N���X </summary>
public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField, Tooltip("���̍U���܂ł̎��ԁi�Ԋu�j")] int _attackInterval = 0;
    [SerializeField, Tooltip("�G�ɗ^����_���[�W")] int _damage = 0;
    [SerializeField, Tooltip("�m�b�N�o�b�N���ɂ������")] int _knockBackPower = 0;
    [SerializeField, Tooltip("�ړ����x")] int _moveSpeed = 0;
    [SerializeField, Tooltip("������")] int _generatorNumber = 1;

    /// <summary>���̍U���܂ł̎���(�Ԋu) </summary>
    public int AttackInterval { get => _attackInterval; set => _attackInterval = value; }
    /// <summary>�ړ����x</summary>
    public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
  
    /// <summary>�I�u�W�F�N�g�̓��� </summary>
    /// <param name="vector3">�i�s����</param>
    public abstract void Move(Vector3 moveDirection);

    /// <summary>���Ԋu���Ƃɕ���𐶐����� </summary>
    /// <param name="playerTransform">�v���C���[�̈ʒu</param>
    public abstract IEnumerator Generator(Transform playerTransform);

    /// <summary>���߂�ꂽ������𐶐����� </summary>
    /// <param name="weaponObject">�������镐��</param>
    /// <param name="playerTransform">�v���C���[�̈ʒu</param>
    public void GameObjectGenerator(GameObject weaponObject, Transform playerTransform)
    {
        for (var i = 0; i < _generatorNumber; i++)
        {
            var offsetX = Random.Range(-1.0f, 1.0f);
            var offsetY = Random.Range(-1.0f, 1.0f);
            var generationPos = new Vector3(playerTransform.position.x + offsetX, playerTransform.position.y + offsetY, 0);
            var go = Instantiate(this, generationPos, Quaternion.identity);
            go.Move(playerTransform.up);
        }
    }

    /// <summary>�G�ɓ���������_���[�W��^���� </summary>
    public void Attack(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            enemyStatus.GetDamage(_damage);
        }
    }

    /// <summary>�G�ɓ���������_���[�W��^���� </summary>
    public void Attack(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();
            enemyStatus.GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
