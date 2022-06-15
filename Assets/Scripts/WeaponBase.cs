using System.Collections;
using UnityEngine;

/// <summary>����̊��N���X </summary>
public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField, Tooltip("���̍U���܂ł̎��ԁi�Ԋu�j")] int _attackInterval = 0;
    [SerializeField, Tooltip("�G�ɗ^����_���[�W")] int _damage = 0;
    [SerializeField, Tooltip("�m�b�N�o�b�N���ɂ������")] int _knockBackPower = 0;
    [SerializeField, Tooltip("�ړ����x")] int _moveSpeed = 0;
    [SerializeField, Tooltip("������")] int _generatorNumber = 1;
    [SerializeField, Tooltip("�ő僌�x��")] int _maxLevel = 9;
    [SerializeField, Tooltip("���x���A�b�v���̋����@��ō�蒼��")] int[] _levelupstatus = default;
    static bool _isGenerate = true;
    /// <summary>���̍U���܂ł̎���(�Ԋu) </summary>
    public int AttackInterval { get => _attackInterval; set => _attackInterval = value; }
    /// <summary>�ړ����x</summary>
    public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    /// <summary>�G�ɗ^����_���[�W </summary>
    public int Damage { get => _damage; set => _damage = value; }
    public static bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }
    public int MaxLevel { get => _maxLevel; set => _maxLevel = value; }

    /// <summary>�I�u�W�F�N�g�̓��� </summary>
    /// <param name="vector3">�i�s����</param>
    public abstract void Move();

    /// <summary>���Ԋu���Ƃɕ���𐶐����� </summary>
    public abstract IEnumerator Generator();

    public abstract void LevelUp(int level);

    /// <summary>���߂�ꂽ������𐶐����� </summary>
    /// <param name="weaponObject">�������镐��</param>
    public void GameObjectGenerator()
    {
        if (!_isGenerate)
        {
            return;
        }

        for (var i = 0; i < _generatorNumber; i++)
        {
            var offsetX = Random.Range(-1.0f, 1.0f);
            var offsetY = Random.Range(-1.0f, 1.0f);
            var generationPos = new Vector3(GameManager.Instance.Player.transform.position.x + offsetX, GameManager.Instance.Player.transform.position.y + offsetY, 0);
            var go = Instantiate(this, generationPos, Quaternion.identity);
            go.Move();
        }
    }

    /// <summary>�G�ɓ���������_���[�W��^���� </summary>
    public void Attack(Collider2D other, bool destroyFlag)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();
            enemyStatus.GetDamage(_damage);

            if (destroyFlag)    //�G�ɓ���������폜����镐��Ȃ�폜����
            {
                Destroy(gameObject);
                GameManager.Instance.RemovePauseObject(gameObject.GetComponent<IPause>());
            }
        }
    }
}
