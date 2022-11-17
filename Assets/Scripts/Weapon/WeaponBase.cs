using System.Collections;
using UnityEngine;

/// <summary>����̊��N���X </summary>
public abstract class WeaponBase : MonoBehaviour, IPause
{
    /// <summary>�U���͈͂̍ŏ��l </summary>
    const float ATTACK_RANGE_MIN_OFFSET = -1.0f;
    /// <summary>�U���͈͂̍ő�l </summary>
    const float ATTACK_RANGE_MAX_OFFSET = 1.0f;

    [SerializeField, Tooltip("���̍U���܂ł̎��ԁi�Ԋu�j")] float _attackInterval = 0;
    [SerializeField, Tooltip("�G�ɗ^����_���[�W")] int _damage = 0;
    [SerializeField, Tooltip("�m�b�N�o�b�N���ɂ������")] int _knockBackPower = 0;
    [SerializeField, Tooltip("�ړ����x")] int _moveSpeed = 0;
    [SerializeField, Tooltip("������")] int _generateNumber = 1;
    [SerializeField, Tooltip("�ő僌�x��")] int _maxLevel = 9;
    [SerializeField, Tooltip("���x���A�b�v���̋���")] int[] _levelupstatus = default;
    /// <summary>���������邩�ǂ��� </summary>
    static bool _isGenerate = true;
    /// <summary>���̍U���܂ł̎���(�Ԋu) </summary>
    public float AttackInterval { get => _attackInterval; set => _attackInterval = value; }
    /// <summary>�ړ����x</summary>
    public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    /// <summary>�G�ɗ^����_���[�W </summary>
    public int Damage { get => _damage; }
    /// <summary>���������邩�ǂ��� </summary>
    public static bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }
    /// <summary>�ő僌�x�� </summary>
    public int MaxLevel { get => _maxLevel; }
    /// <summary>������ </summary>
    public int GenerateNumber { get => _generateNumber; set => _generateNumber = value; }

    /// <summary>�I�u�W�F�N�g�̓��� </summary>
    public abstract void Move();

    /// <summary>���Ԋu���Ƃɕ���𐶐����� </summary>
    public abstract IEnumerator Generator();
    /// <summary>���x���A�b�v�ɑI�΂ꂽ���ɌĂ΂�� </summary>
    public abstract void LevelUp(int level);
    /// <summary>���������X�e�[�^�X�������ɖ߂� </summary>
    public abstract void ResetStatus();

    /// <summary>���߂�ꂽ������𐶐����� </summary>
    public void GameObjectGenerator()
    {
        if (!_isGenerate)
        {
            return;
        }

        var generateNumber = _generateNumber + GameManager.Instance.Player.PlayerStatus[4];     //������

        for (var i = 0; i < generateNumber; i++)
        {
            var offsetX = Random.Range(ATTACK_RANGE_MIN_OFFSET, ATTACK_RANGE_MAX_OFFSET);
            var offsetY = Random.Range(ATTACK_RANGE_MIN_OFFSET, ATTACK_RANGE_MAX_OFFSET);
            var generationPos = new Vector3(GameManager.Instance.Player.transform.position.x + offsetX, GameManager.Instance.Player.transform.position.y + offsetY, 0);
            var go = Instantiate(this, generationPos, Quaternion.identity);
            go.Move();
        }
    }

    /// <summary>�G�ɓ���������_���[�W��^���� </summary>
    /// <param name="hitCollider">�Փ˕�</param>
    /// <param name="destroyFlag">�U���㕐���j�󂷂邩�ǂ���</param>
    public void Attack(Collider2D hitCollider, bool destroyFlag)
    {
        if (hitCollider.CompareTag("Enemy"))
        {
            var enemy = hitCollider.GetComponent<EnemyController>();
            enemy.GetDamage(_damage + (int)GameManager.Instance.Player.PlayerStatus[4]);

            if (destroyFlag)    //�G�ɓ���������폜����镐��Ȃ�폜����
            {
                Destroy(gameObject);
                GameManager.Instance.RemovePauseObject(gameObject.GetComponent<IPause>());
            }
        }
    }

    public virtual void Pause()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Restart()
    {
        throw new System.NotImplementedException();
    }
}
