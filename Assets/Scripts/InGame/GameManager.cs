using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

/// <summary>ゲーム全体を管理するクラス シングルトンパターンを使用</summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = default;
 
    [SerializeField, Tooltip("プレイヤープレハブ")] PlayerController _playerPrefab = default;
    /// <summary>生成されたプレイヤー </summary>
    PlayerController _player = default;
    /// <summary>経験値アイテムを生成するスポナー </summary>
    EXPSpawner _expSpawner = default;
    /// <summary>プレイヤーの武器を管理するクラス </summary>
    WeaponManager _weaponManager = default;
    /// <summary>ゲーム内時間を管理するクラス </summary>
    GameTimeManager _gameTimeManager = default;
    /// <summary>ゲーム内で使用するキャンバス</summary>
    GameObject _gameCanvas = default;

    /// <summary>
    /// ゲーム結果を表示するキャンバス
    /// <para>子オブジェクト 0=背景 1=結果を表示するテキスト 2=経過時間を表示するテキスト 3=レベルを表示するテキスト 4=タイトルに戻るボタン</para>
    /// </summary>
    GameObject _judgementCanvas = default;

    /// <summary>プレイヤーを写すカメラ </summary>
    CinemachineVirtualCamera _playerCamera;
    /// <summary>ポーズするオブジェクト </summary>
    List<IPause> _pauseObjects = new List<IPause>();
    /// <summary>初期武器 </summary>
    Weapons _firstWeapon = 0;
    /// <summary>生成されたプレイヤー </summary>
    public PlayerController Player { get => _player; }
    /// <summary>経験値アイテムを生成するスポナー </summary>
    public EXPSpawner ExpSpawner { get => _expSpawner; }
    /// <summary>プレイヤーの武器を管理するクラス </summary>
    public WeaponManager WeaponManager { get => _weaponManager; }

    public static GameManager Instance { get => _instance; }
    /// <summary>初期武器 </summary>
    public Weapons FirstWeapon { get => _firstWeapon; set => _firstWeapon = value; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            SceneManager.sceneLoaded += GameSceneLoad;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>シーン遷移時の処理</summary>
    public void GameSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")  //ゲームシーンに遷移したらゲーム開始の処理を行う
        {
            GameStartProcess();
        }
        else
        {
            _expSpawner = null;
            _weaponManager = null;
        }
    }

    /// <summary>初期武器を使用可能にする </summary>
    public void SetFirstWeaponLevel()
    {
        foreach (var data in GameData.WeaponSelectTables)
        {
            if (data.Id == (int)_firstWeapon)
            {
                data.Level++;
                break;
            }
        }
    }

    /// <summary>全武器のレベルを初期化する </summary>
    public void ResetWeaponLevel()
    {
        foreach (var data in GameData.WeaponSelectTables)
        {
            data.Level = 0;
        }
    }

    /// <summary>ゲームオーバー処理 </summary>
    public void GameOver()
    {
        ResultProcess();
        _judgementCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Game Over";
    }

    /// <summary>ゲームクリア処理</summary>
    public void GameClear()
    {
        ResultProcess();

        //背景の色を変更しプレイヤーにクリアしたことを伝える
        _judgementCanvas.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        _judgementCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Game Clear";
    }

    /// <summary>ポーズをさせるオブジェクトをリストに追加する </summary>
    /// <param name="obj">ポーズさせるオブジェクト</param>
    public void AddPauseObject(IPause obj)
    {
        _pauseObjects.Add(obj);
    }

    /// <summary>ポーズをさせるオブジェクトをリストから削除する </summary>
    /// <param name="obj"></param>
    public void RemovePauseObject(IPause obj)
    {
        _pauseObjects.Remove(obj);
    }

    /// <summary>ポーズ</summary>
    public void Pause()
    {
        foreach (var obj in _pauseObjects)
        {
            obj.Pause();
        }
    }

    /// <summary>ポーズ解除 </summary>
    public void Restart()
    {
        foreach (var obj in _pauseObjects)
        {
            obj.Restart();
        }
    }

    /// <summary>ゲームを開始させる為に必要な処理 </summary>
    void GameStartProcess()
    {
        _player = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity); 

        //ゲームシーンに存在するものを取得する
        _expSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EXPSpawner>();
        _weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        _playerCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        _gameCanvas = GameObject.Find("GameCanvas");
        _judgementCanvas = GameObject.Find("JudgementCanvas");
        _gameTimeManager = GameObject.Find("TimeManager").GetComponent<GameTimeManager>();

        //プレイヤー武器関連の処理を行う
        _weaponManager.GetWeapon((int)_firstWeapon, WeaponType.Weapon);
        _weaponManager.ResetWeapons();
        SetFirstWeaponLevel();
        WeaponBase.IsGenerate = true;

        //オブジェクトを取得してから行う処理
        _judgementCanvas.SetActive(false);
        _playerCamera.Follow = _player.transform;
    }

    /// <summary>ゲーム終了が終了したことをプレイヤーに伝える</summary>
    void ResultProcess()
    {
        Pause();
        _gameCanvas.SetActive(false);
        _judgementCanvas.SetActive(true);

        //経過時間とレベルを表示する
        _judgementCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{_gameTimeManager.Minutes}:{_gameTimeManager.Seconds.ToString("D2")}";
        _judgementCanvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Lv.{_player.CurrentLevel}";
    }
}
