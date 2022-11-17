using System.Collections.Generic;
using UnityEngine;

/// <summary>ObjectPool�̊��N���X </summary>
public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField, Tooltip("�v�[������I�u�W�F�N�g")] GameObject _targetObj = default;
    [SerializeField, Tooltip("�ŏ��ɐ��������")] int _maxCount = 0;
    /// <summary>�������ꂽ�I�u�W�F�N�g�̃��X�g </summary>
    List<GameObject> _targetList = new List<GameObject>();
    /// <summary>�������ꂽ�I�u�W�F�N�g�̃��X�g </summary>
    public List<GameObject> TargetList { get => _targetList; }

    /// <summary>�I�u�W�F�N�g���o��</summary>
    /// <param name="spawnPoint">�X�|�[���n�_</param>
    public abstract GameObject Spawn(Vector2 spawnPoint);

    /// <summary>�X�|�[���n�_�����߂�</summary>
    /// <returns>�X�|�[���n�_</returns>
    public abstract Vector2 SetSpawnPoint();

    /// <summary>���������X�g�ɒǉ����� </summary>
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
