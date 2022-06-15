using UnityEngine;
using TMPro;

/// <summary>ステージに進入時から経過時間を表示する </summary>
public class GameTimeManager : MonoBehaviour, IPause
{
    private const int ONE_SECOND = 1;
    private const int UPDATE_MINUTES_TIME = 59;

    [SerializeField, Tooltip("時間を表示するテキスト")] TextMeshProUGUI _textMeshProUGUI = default;
    int _minutes = 0;
    int _seconds = 0;
    float _time = 0;
    bool _isPause = false;

    void Start()
    {
        _textMeshProUGUI.text = $"{_minutes}:{_seconds.ToString("D2")}";
        GameManager.Instance.AddPauseObject(this);
    }

    void Update()
    {
        if (!_isPause)
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

    public void Pause()
    {
        _isPause = true;
    }

    public void Restart()
    {
        _isPause = false;
    }

}
