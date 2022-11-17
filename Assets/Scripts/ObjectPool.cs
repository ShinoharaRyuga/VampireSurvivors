using System.Collections.Generic;
using UnityEngine;

/// <summary>ObjectPoolの基底クラス </summary>
public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField, Tooltip("プールするオブジェクト")] GameObject _targetObj = default;
    [SerializeField, Tooltip("最初に生成する個数")] int _maxCount = 0;
    /// <summary>生成されたオブジェクトのリスト </summary>
    List<GameObject> _targetList = new List<GameObject>();
    /// <summary>生成されたオブジェクトのリスト </summary>
    public List<GameObject> TargetList { get => _targetList; }

    /// <summary>オブジェクトを出す</summary>
    /// <param name="spawnPoint">スポーン地点</param>
    public abstract GameObject Spawn(Vector2 spawnPoint);

    /// <summary>スポーン地点を決める</summary>
    /// <returns>スポーン地点</returns>
    public abstract Vector2 SetSpawnPoint();

    /// <summary>生成しリストに追加する </summary>
    public void SetUp()
    {
        for (var i = 0; i < _maxCount; i++)
        {
            var go = Instantiate(_targetObj, Vector2.zero, Quaternion.identity);
            _targetList.Add(go);
            go.SetActive(false);
        }
    }
}
