using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : WeaponBase
{
    [SerializeField, Tooltip("プレイヤーからの最大距離")] float _maxOffset = 0f;
    [SerializeField, Tooltip("プレイヤーからの最小距離")] float _minOffset = 0f;
    Vector2 _moveDirection = default;
    Vector3 _movePoint = default;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();


   
    public override IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            GameObjectGenerator();
        }

    }

    public override void LevelUp(int level)
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        SetMovePoint();
        var distance = Vector3.Distance(transform.position, _movePoint);
        if (distance <= 2f)
        {
            _rb2D.velocity = Vector3.zero;
            Debug.Log("到着");
        }
    }

    public void SetMovePoint()
    {
        var offsetX = Random.Range(_minOffset, _maxOffset);
        var offsetY = Random.Range(_minOffset, _maxOffset);
        _movePoint = new Vector3(offsetX, offsetY, 0);
        Debug.Log($"{offsetX} {offsetY}");
        _moveDirection = (_movePoint - transform.position).normalized * MoveSpeed;
        transform.up = _moveDirection;
        _rb2D.velocity = _moveDirection;
    }

    public override void ResetStatus()
    {
     
    }
}
