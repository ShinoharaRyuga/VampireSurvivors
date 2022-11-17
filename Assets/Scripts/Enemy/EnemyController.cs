using UnityEngine;

/// <summary>�G�𐧌䂷��N���X</summary>
[RequireComponent(typeof(Animator), typeof(AudioSource), typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("�̗�")] int _hp = 1;
    [SerializeField, Tooltip("�U����")] int _attackPower = 1;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 1f;
    [SerializeField, Tooltip("�_���[�WSE")] AudioClip _damageSE = default;
    /// <summary>�ړ��\���ǂ���</summary>
    bool _isMove = true;

    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    Animator _anim => GetComponent<Animator>();
    AudioSource _audioSource => GetComponent<AudioSource>();

    /// <summary>�̗�</summary>
    public int Hp { get => _hp; set => _hp = value; }

    private void FixedUpdate()
    {
        //�ړ�����
        if (_isMove)
        {
            var dir = (GameManager.Instance.Player.transform.position - transform.position).normalized * _moveSpeed;
            _rb2D.velocity = dir;
        }
        else
        {
            _rb2D.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))      //Player�Ƀ_���[�W��^����
        {
            GameManager.Instance.Player.GetDamage(_attackPower);
        }
        else if (collision.gameObject.CompareTag("Wall"))   //�ǏՓ˂����ꍇ������
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damage">��_���[�W</param>
    public void GetDamage(int damage)
    {
        _hp -= damage;
        _anim.SetTrigger("Damage");
        _audioSource.PlayOneShot(_damageSE);

        if (_hp <= 0)   //���S
        {
            GameManager.Instance.ExpSpawner.Spawn(transform);
            GameManager.Instance.RemovePauseObject(this);
            gameObject.SetActive(false);
        }
    }

    /// <summary>���g�̈ʒu���X�|�[���n�_�Ɉړ������� </summary>
    /// <param name="spawnPoint">�X�|�[���n�_</param>
    public void SetSpawnPoint(Vector2 spawnPoint)
    {
        transform.position = spawnPoint;
    }

    public void Pause()
    {
        _isMove = false;
    }

    public void Restart()
    {

        _isMove = true;
    }
}
