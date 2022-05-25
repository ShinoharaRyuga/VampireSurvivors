using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;
    [SerializeField] PlayerController _player = default;
    int[] _selectedCharacterStatus = new int[16];

    public int[] SelectedCharacterStatus { get => _selectedCharacterStatus; set => _selectedCharacterStatus = value; }

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
            var go = Instantiate(_player, Vector2.zero, Quaternion.identity);
           go.CharacterStatusArray = _selectedCharacterStatus;
        }
    }
}
