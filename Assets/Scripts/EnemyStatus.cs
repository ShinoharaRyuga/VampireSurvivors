using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("�̗�")] int _hp = 1;
    [SerializeField, Tooltip("�U����")] int _attackPower = 1;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 1f;
    [SerializeField, Tooltip("���S���ɗ��Ƃ��o���l�̒l")] int _dropEXPValue = 1;
    [SerializeField, Tooltip("EXP�I�u�W�F�N�g ������")] SetEXP _expObj = default;
    Rigidbody2D _rb2D = default;

    bool _isMove = true;
    public int DropEXPValue { get => _dropEXPValue; }

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        GameManager.Instance.AddPauseObject(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _isMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            _isMove = false;
        }
    }

    private void FixedUpdate()
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Player.GetDamage(_attackPower);
        }
    }

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damage">��_���[�W</param>
    public void GetDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)   //���S
        {
            GameManager.Instance.ExpSpawner.Instantiate(transform);
            gameObject.SetActive(false);
        }
    }

    public void SetPopPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
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
