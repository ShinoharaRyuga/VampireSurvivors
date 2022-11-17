using UnityEngine;

/// <summary>敵を制御するクラス</summary>
[RequireComponent(typeof(Animator), typeof(AudioSource), typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("体力")] int _hp = 1;
    [SerializeField, Tooltip("攻撃力")] int _attackPower = 1;
    [SerializeField, Tooltip("移動速度")] float _moveSpeed = 1f;
    [SerializeField, Tooltip("ダメージSE")] AudioClip _damageSE = default;
    /// <summary>移動可能かどうか</summary>
    bool _isMove = true;

    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    Animator _anim => GetComponent<Animator>();
    AudioSource _audioSource => GetComponent<AudioSource>();

    /// <summary>体力</summary>
    public int Hp { get => _hp; set => _hp = value; }

    private void FixedUpdate()
    {
        //移動処理
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
        if (collision.gameObject.CompareTag("Player"))      //Playerにダメージを与える
        {
            GameManager.Instance.Player.GetDamage(_attackPower);
        }
        else if (collision.gameObject.CompareTag("Wall"))   //壁衝突した場合消える
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>ダメージを受ける </summary>
    /// <param name="damage">被ダメージ</param>
    public void GetDamage(int damage)
    {
        _hp -= damage;
        _anim.SetTrigger("Damage");
        _audioSource.PlayOneShot(_damageSE);

        if (_hp <= 0)   //死亡
        {
            GameManager.Instance.ExpSpawner.Spawn(transform);
            GameManager.Instance.RemovePauseObject(this);
            gameObject.SetActive(false);
        }
    }

    /// <summary>自身の位置をスポーン地点に移動させる </summary>
    /// <param name="spawnPoint">スポーン地点</param>
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
