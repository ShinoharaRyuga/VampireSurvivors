using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�̗�, ��, �A�[�}�[, �ړ����x, �З�, �G���A, ���x, ��������, ��,�@�N�[���_�E��, �^�C,�@����, ���~, ��, ���� ��������̓Y����")]
    int[] _characterStatus = new int[16];

    public void SetStatus()
    {
        GameManager.Instance.SelectedCharacterStatus = _characterStatus;
    }
}
