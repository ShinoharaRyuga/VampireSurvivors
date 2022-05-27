using UnityEngine;
using TMPro;

/// <summary>�X�e�[�W�ɐi��������o�ߎ��Ԃ�\������ </summary>
public class GameTimeManager : MonoBehaviour
{
    private const int ONE_SECOND = 1;
    private const int UPDATE_MINUTES_TIME = 59;

    [SerializeField, Tooltip("���Ԃ�\������e�L�X�g")] TextMeshProUGUI _textMeshProUGUI = default;
    int _minutes = 0;
    int _seconds = 0;

    float _time = 0;
    void Start()
    {
        _textMeshProUGUI.text = $"{_minutes}:{_seconds.ToString("D2")}";
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= ONE_SECOND)
        {
            _time = 0;
            _seconds++;

            if (_seconds >= UPDATE_MINUTES_TIME)
            {
                _seconds = 0;
                _minutes++;
            }

            _textMeshProUGUI.text = $"{_minutes}:{_seconds.ToString("D2")}";
        }
    }
}
