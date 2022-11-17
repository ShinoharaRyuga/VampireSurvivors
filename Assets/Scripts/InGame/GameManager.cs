using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

/// <summary>�Q�[���S�̂��Ǘ�����N���X �V���O���g���p�^�[�����g�p</summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = default;
 
    [SerializeField, Tooltip("�v���C���[�v���n�u")] PlayerController _playerPrefab = default;
    /// <summary>�������ꂽ�v���C���[ </summary>
    PlayerController _player = default;
    /// <summary>�o���l�A�C�e���𐶐�����X�|�i�[ </summary>
    EXPSpawner _expSpawner = default;
    /// <summary>�v���C���[�̕�����Ǘ�����N���X </summary>
    WeaponManager _weaponManager = default;
    /// <summary>�Q�[�������Ԃ��Ǘ�����N���X </summary>
    GameTimeManager _gameTimeManager = default;
    /// <summary>�Q�[�����Ŏg�p����L�����o�X</summary>
    GameObject _gameCanvas = default;

    /// <summary>
    /// �Q�[�����ʂ�\������L�����o�X
    /// <para>�q�I�u�W�F�N�g 0=�w�i 1=���ʂ�\������e�L�X�g 2=�o�ߎ��Ԃ�\������e�L�X�g 3=���x����\������e�L�X�g 4=�^�C�g���ɖ߂�{�^��</para>
    /// </summary>
    GameObject _judgementCanvas = default;

    /// <summary>�v���C���[���ʂ��J���� </summary>
    CinemachineVirtualCamera _playerCamera;
    /// <summary>�|�[�Y����I�u�W�F�N�g </summary>
    List<IPause> _pauseObjects = new List<IPause>();
    /// <summary>�������� </summary>
    Weapons _firstWeapon = 0;
    /// <summary>�������ꂽ�v���C���[ </summary>
    public PlayerController Player { get => _player; }
    /// <summary>�o���l�A�C�e���𐶐�����X�|�i�[ </summary>
    public EXPSpawner ExpSpawner { get => _expSpawner; }
    /// <summary>�v���C���[�̕�����Ǘ�����N���X </summary>
    public WeaponManager WeaponManager { get => _weaponManager; }

    public static GameManager Instance { get => _instance; }
    /// <summary>�������� </summary>
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

    /// <summary>�V�[���J�ڎ��̏���</summary>
    public void GameSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")  //�Q�[���V�[���ɑJ�ڂ�����Q�[���J�n�̏������s��
        {
            GameStartProcess();
        }
        else
        {
            _expSpawner = null;
            _weaponManager = null;
        }
    }

    /// <summary>����������g�p�\�ɂ��� </summary>
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

    /// <summary>�S����̃��x�������������� </summary>
    public void ResetWeaponLevel()
    {
        foreach (var data in GameData.WeaponSelectTables)
        {
            data.Level = 0;
        }
    }

    /// <summary>�Q�[���I�[�o�[���� </summary>
    public void GameOver()
    {
        ResultProcess();
        _judgementCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Game Over";
    }

    /// <summary>�Q�[���N���A����</summary>
    public void GameClear()
    {
        ResultProcess();

        //�w�i�̐F��ύX���v���C���[�ɃN���A�������Ƃ�`����
        _judgementCanvas.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        _judgementCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Game Clear";
    }

    /// <summary>�|�[�Y��������I�u�W�F�N�g�����X�g�ɒǉ����� </summary>
    /// <param name="obj">�|�[�Y������I�u�W�F�N�g</param>
    public void AddPauseObject(IPause obj)
    {
        _pauseObjects.Add(obj);
    }

    /// <summary>�|�[�Y��������I�u�W�F�N�g�����X�g����폜���� </summary>
    /// <param name="obj"></param>
    public void RemovePauseObject(IPause obj)
    {
        _pauseObjects.Remove(obj);
    }

    /// <summary>�|�[�Y</summary>
    public void Pause()
    {
        foreach (var obj in _pauseObjects)
        {
            obj.Pause();
        }
    }

    /// <summary>�|�[�Y���� </summary>
    public void Restart()
    {
        foreach (var obj in _pauseObjects)
        {
            obj.Restart();
        }
    }

    /// <summary>�Q�[�����J�n������ׂɕK�v�ȏ��� </summary>
    void GameStartProcess()
    {
        _player = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity); 

        //�Q�[���V�[���ɑ��݂�����̂��擾����
        _expSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EXPSpawner>();
        _weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        _playerCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        _gameCanvas = GameObject.Find("GameCanvas");
        _judgementCanvas = GameObject.Find("JudgementCanvas");
        _gameTimeManager = GameObject.Find("TimeManager").GetComponent<GameTimeManager>();

        //�v���C���[����֘A�̏������s��
        _weaponManager.GetWeapon((int)_firstWeapon, WeaponType.Weapon);
        _weaponManager.ResetWeapons();
        SetFirstWeaponLevel();
        WeaponBase.IsGenerate = true;

        //�I�u�W�F�N�g���擾���Ă���s������
        _judgementCanvas.SetActive(false);
        _playerCamera.Follow = _player.transform;
    }

    /// <summary>�Q�[���I�����I���������Ƃ��v���C���[�ɓ`����</summary>
    void ResultProcess()
    {
        Pause();
        _gameCanvas.SetActive(false);
        _judgementCanvas.SetActive(true);

        //�o�ߎ��Ԃƃ��x����\������
        _judgementCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{_gameTimeManager.Minutes}:{_gameTimeManager.Seconds.ToString("D2")}";
        _judgementCanvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Lv.{_player.CurrentLevel}";
    }
}
