using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�̗�, ��, �A�[�}�[, �ړ����x, �З�, �G���A, ���x, ��������, ��,�@�N�[���_�E��, �^�C,�@����, ���~, ��, ���� ��������̓Y����")]
    float[] _characterStatus = new float[16];

    public void SetStatus()
    {
        GameManager.Instance.SelectedCharacterStatus = _characterStatus;
    }
}
