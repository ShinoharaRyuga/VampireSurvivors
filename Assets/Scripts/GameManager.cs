using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;
    [SerializeField] PlayerController _playerPrefab = default;
    /// <summary>�������ꂽ�v���C���[ </summary>
    PlayerController _player = default;
    EXPSpawner _expSpawner = default;
    int[] _selectedCharacterStatus = new int[16];

    public int[] SelectedCharacterStatus { get => _selectedCharacterStatus; set => _selectedCharacterStatus = value; }
    /// <summary>�������ꂽ�v���C���[ </summary>
    public PlayerController Player { get => _player; set => _player = value; }
    public EXPSpawner ExpSpawner { get => _expSpawner; set => _expSpawner = value; }

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

    public void GameSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            _player = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity);
            _player.CharacterStatusArray = _selectedCharacterStatus;
            _expSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EXPSpawner>();
        }
    }
}