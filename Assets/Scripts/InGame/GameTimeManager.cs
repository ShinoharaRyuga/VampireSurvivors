using UnityEngine;
using TMPro;

/// <summary>ステージに進入時からの経過時間を表示する </summary>
public class GameTimeManager : MonoBehaviour, IPause
{
    /// <summary>時間計測用の一秒を定義 </summary>
    const float ONE_SECOND = 1f;
    /// <summary>一分を定義</summary>
    const float MINUTES_TIME = 59f;

    [SerializeField, Tooltip("時間を表示するテキスト")] TextMeshProUGUI _timerText = default;
    [SerializeField, Tooltip("敵のスポナー")] EnemySpawner _enemySpawner = default;
    [SerializeField, Tooltip("クリアタイム")] float _clearTime = 1f;

    float _time = 0;
    /// <summary>経過時間(分) </summary>
    int _minutes = 0;
    /// <summary>経過時間(秒) </summary>
    int _seconds = 0;
    /// <summary>ポーズ中かどうか</summary>
    bool _isPause = false;

    /// <summary>経過時間(分) </summary>
    public int Minutes { get => _minutes; }
    /// <summary>経過時間(秒) </summary>
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

            if (_time >= ONE_SECOND)　   //一秒経過
            {
                _time = 0;
                _seconds++;

                if (_seconds >= MINUTES_TIME)　//一分経過
                {
                    _seconds = 0;
                    _enemySpawner.UpgradeSpawner();
                    _minutes++;
                }

                _timerText.text = $"{_minutes}:{_seconds.ToString("D2")}";  //時間を表示
            }

            if (_minutes >= _clearTime)     //ゲームクリア
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
