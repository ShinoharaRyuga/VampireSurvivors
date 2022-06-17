using System.Collections;
using UnityEngine;

public class Whip : WeaponBase, IPause
{
    [SerializeField, Tooltip("Œø‰ÊŽžŠÔ")] float _effectTime = 2f;
    GameObject _leftWhip = default;
    GameObject _rightWhip = default;

    void Start()
    {
       GameManager.Instance.AddPauseObject(this);
    }

    private void Update()
    {
        transform.position = GameManager.Instance.Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, false);
    }


    public override IEnumerator Generator()
    {
        var go = Instantiate(gameObject, GameManager.Instance.Player.transform.position, Quaternion.identity);
        _leftWhip = go.transform.GetChild(0).gameObject;
        _rightWhip = go.transform.GetChild(1).gameObject;
        yield return new WaitForSeconds(_effectTime);
        yield return new WaitUntil(() => IsGenerate == true);
        go.SetActive(false);

        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            yield return new WaitUntil(() => IsGenerate == true);
            go.SetActive(true);
            Move();
            yield return new WaitForSeconds(_effectTime);
            yield return new WaitUntil(() => IsGenerate == true);
            go.SetActive(false);
        }
    }

    public override void Move()
    {
        if (GameManager.Instance.Player.transform.up.x < 0)
        {
            _leftWhip.SetActive(true);
            _rightWhip.SetActive(false);
        }
        else if(GameManager.Instance.Player.transform.up.x > 0)
        {
            _leftWhip.SetActive(false);
            _rightWhip.SetActive(true);
        }
        else
        {
            _leftWhip.SetActive(true);
            _rightWhip.SetActive(false);
        }
    }

    public override void LevelUp(int level)
    {
        Debug.Log($"•Ú{level}");
    }

    public void Pause()
    {
        IsGenerate = false;
    }

    public void Restart()
    {
        IsGenerate = true;
    }

    public override void ResetStatus()
    {
        
    }
}
