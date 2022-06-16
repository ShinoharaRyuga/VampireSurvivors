using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;
    [SerializeField] PlayerController _playerPrefab = default;
    /// <summary>生成されたプレイヤー </summary>
    PlayerController _player = default;
    EXPSpawner _expSpawner = default;
    WeaponManager _weaponManager = default;
    GameTimeManager _gameTimeManager = default;
    GameObject _gameCanvas = default;
    GameObject _judgementCanvas = default;
    CinemachineVirtualCamera _playerCamera;
    float[] _selectedCharacterStatus = new float[16];
    /// <summary>ポーズするオブジェクト </summary>
    List<IPause> _pauseObjects = new List<IPause>();
    public float[] SelectedCharacterStatus { get => _selectedCharacterStatus; set => _selectedCharacterStatus = value; }
    /// <summary>生成されたプレイヤー </summary>
    public PlayerController Player { get => _player; set => _player = value; }
    public EXPSpawner ExpSpawner { get => _expSpawner; set => _expSpawner = value; }
    public WeaponManager WeaponManager { get => _weaponManager; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            SceneManager.sceneLoaded += GameSceneLoad;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Restart();
        }
    }

    public void GameSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            _player = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity);
            _player.CharacterStatusArray = _selectedCharacterStatus;
            _expSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EXPSpawner>();
            _weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
            _playerCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
            _gameCanvas = GameObject.Find("GameCanvas");
            _judgementCanvas = GameObject.Find("JudgementCanvas");
            _gameTimeManager = GameObject.Find("TimeManager").GetComponent<GameTimeManager>();
            _judgementCanvas.SetActive(false);
            _playerCamera.Follow = _player.transform;
            _weaponManager.GetWeapon((int)_selectedCharacterStatus[15], WeaponType.Weapon);
            SetFirstWeaponLevel();
        }
    }

    public void SetFirstWeaponLevel()
    {
        foreach (var data in GameData.SkillSelectTables)
        {
            if (data.Id == _selectedCharacterStatus[15])
            {
                data.Level++;
                break;
            }
        }
    }

    public void GameOver()
    {
        _gameCanvas.SetActive(false);
        _judgementCanvas.SetActive(true);
        _judgementCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{_gameTimeManager.Minutes}:{_gameTimeManager.Seconds.ToString("D2")}";
        _judgementCanvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Lv.{_player.CurrentLevel}";
        Pause();
    }

    public void GameClear()
    {
        _gameCanvas.SetActive(false);
        _judgementCanvas.SetActive(true);
        _judgementCanvas.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        _judgementCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Game Clear";
        _judgementCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{_gameTimeManager.Minutes}:{_gameTimeManager.Seconds.ToString("D2")}";
        _judgementCanvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Lv.{_player.CurrentLevel}";
        Pause();
    }

    /// <summary>ポーズをさせるオブジェクトを取得しリストに追加する </summary>
    /// <param name="pause">ポーズさせるオブジェクト</param>
    public void AddPauseObject(IPause pause)
    {
        _pauseObjects.Add(pause);
    }

    public void RemovePauseObject(IPause obj)
    {
        _pauseObjects.Remove(obj);
    }

    /// <summary>ポーズ</summary>
    public void Pause()
    {
        Array.ForEach(_pauseObjects.ToArray(), p => p.Pause());
    }

    /// <summary>ポーズ解除 </summary>
    public void Restart()
    {
        Array.ForEach(_pauseObjects.ToArray(), p => p.Restart());
    }
}
