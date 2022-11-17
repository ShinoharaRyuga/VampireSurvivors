using UnityEngine;
using TMPro;

/// <summary>�X�e�[�W�ɐi��������̌o�ߎ��Ԃ�\������ </summary>
public class GameTimeManager : MonoBehaviour, IPause
{
    /// <summary>���Ԍv���p�̈�b���` </summary>
    const float ONE_SECOND = 1f;
    /// <summary>�ꕪ���`</summary>
    const float MINUTES_TIME = 59f;

    [SerializeField, Tooltip("���Ԃ�\������e�L�X�g")] TextMeshProUGUI _timerText = default;
    [SerializeField, Tooltip("�G�̃X�|�i�[")] EnemySpawner _enemySpawner = default;
    [SerializeField, Tooltip("�N���A�^�C��")] float _clearTime = 1f;

    float _time = 0;
    /// <summary>�o�ߎ���(��) </summary>
    int _minutes = 0;
    /// <summary>�o�ߎ���(�b) </summary>
    int _seconds = 0;
    /// <summary>�|�[�Y�����ǂ���</summary>
    bool _isPause = false;

    /// <summary>�o�ߎ���(��) </summary>
    public int Minutes { get => _minutes; }
    /// <summary>�o�ߎ���(�b) </summary>
    public int Seconds { get => _seconds; }

    void Start()
    {
        _timerText.text = $"{_minutes}:{_seconds.ToString("D2")}";
        GameManager.Instance.AddPauseObject(this);
    }

    void Update()
    {
        if (!_isPause)
        {
            _time += Time.deltaTime;

            if (_time >= ONE_SECOND)�@   //��b�o��
            {
                _time = 0;
                _seconds++;

                if (_seconds >= MINUTES_TIME)�@//�ꕪ�o��
                {
                    _seconds = 0;
                    _enemySpawner.UpgradeSpawner();
                    _minutes++;
                }

                _timerText.text = $"{_minutes}:{_seconds.ToString("D2")}";  //���Ԃ�\��
            }

            if (_minutes >= _clearTime)     //�Q�[���N���A
            {
                GameManager.Instance.GameClear();
            }
        }       
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Restart()
    {
        _isPause = false;
    }

}
