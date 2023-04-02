using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectModeController : MonoBehaviour
{
    [SerializeField] private GameObject _gameSetupPrefab, _distanceMiniGamePrefab;
    private GameSetup _gameSetup;
    private DistanceMiniGameController _distanceMiniGameController;
    [SerializeField] private GameObject _gameModes;
    [SerializeField] private GameObject _miniGames;
    [SerializeField] private GameObject _mapSelector;
    [SerializeField] private GameObject _carSelector;
    [SerializeField] private GameObject _countPlayersSettings;
    [SerializeField] private GameObject _countLapsSettings;

    [SerializeField] private Image _carImage;
    [SerializeField] private Text _carName;
    [SerializeField] private Image _mapImage;
    [SerializeField] private Text _mapName;

    [SerializeField] private Text _countPlayersText;
    [SerializeField] private Text _countLapsText;

    [SerializeField] private Button _playButton;

    [SerializeField] private CarDatabase _carDatabase;
    [SerializeField] private MapDatabase _mapDatabase;

    [SerializeField] private int _maxCountPlayers;
    [SerializeField] private int _maxCountLaps;

    private int _currCarIndex;
    private MapMenuItem[] currMaps;
    private int _currMapIndex;
    private GameMode _currentGameMode;
    private int _countPlayers = 1;
    private int _countLaps = 1;

    private List<CarMenuItem> _aviableCars = new List<CarMenuItem>();

    private void Start()
    {
        foreach (var carMenuItem in _carDatabase.cars)
        {
            if(carMenuItem.PlayerCarPrefab!=null)
                _aviableCars.Add(carMenuItem);
        }
    }

    public void ShowGameSettings()
    {
        _gameSetup = Instantiate(_gameSetupPrefab).GetComponent<GameSetup>();
        _mapSelector.SetActive(true);
        _carSelector.SetActive(true);
        _playButton.gameObject.SetActive(true);
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(StartGame);
        _countPlayersSettings.SetActive(true);
        if(_currentGameMode==GameMode.Ring)
            _countLapsSettings.SetActive(true);
        
        if (_currentGameMode == GameMode.Sprint)
            currMaps = _mapDatabase.sprintMaps;
        else
            currMaps = _mapDatabase.ringMaps;
        
        ShowCarItem();
        ShowMapItem();
    }

    public void ShowMiniGameSettings()
    {
        _distanceMiniGameController = Instantiate(_distanceMiniGamePrefab).GetComponent<DistanceMiniGameController>();
        currMaps = new MapMenuItem[1];
        currMaps[0] = _mapDatabase._distanceMiniGame;
        
        _carSelector.SetActive(true);
        _playButton.gameObject.SetActive(true);
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(StartMiniGame);
        ShowCarItem();
    }

    public void ShowCarItem()
    {
        CarMenuItem item = _aviableCars[_currCarIndex];
        _carImage.sprite = item.Sprite;
        _carName.text = item.Name;
    }
    
    public void ShowMapItem()
    {
        MapMenuItem item = currMaps[_currMapIndex];
        _mapImage.sprite = item.Sprite;
        _mapName.text = item.Name;
    }

    public void NextCar()
    {
        _currCarIndex++;
        _currCarIndex = _currCarIndex % _aviableCars.Count;
        ShowCarItem();
    }

    public void PrevCar()
    {
        _currCarIndex--;
        if (_currCarIndex < 0)
            _currCarIndex = _aviableCars.Count - 1;
        ShowCarItem();
    }
    
    public void NextMap()
    {
        _currMapIndex++;
        _currMapIndex = _currMapIndex % currMaps.Length;
        ShowMapItem();
    }

    public void PrevMap()
    {
        _currMapIndex--;
        if (_currMapIndex < 0)
            _currMapIndex = currMaps.Length - 1;
        ShowMapItem();
    }
    
    public void IncCountPlayers()
    {
        if(_countPlayers<_maxCountPlayers)
            _countPlayers++;
        SetCountPlayers();
    }

    public void DecCountPlayers()
    {
        if(_countPlayers>1)
            _countPlayers--;
        SetCountPlayers();
    }
    
    public void IncCountLaps()
    {
        if(_countLaps<_maxCountLaps)
            _countLaps++;
        SetCountLaps();
    }

    public void DecCountLaps()
    {
        if(_countLaps>1)
            _countLaps--;
        SetCountLaps();
    }

    public void StartGame()
    {
        print("start game");
        _gameSetup.GameMode = _currentGameMode;
        _gameSetup.PlayerCarPrefab = _aviableCars[_currCarIndex].PlayerCarPrefab;
        _gameSetup.CountLaps = _countLaps;
        _gameSetup.CountPlayers = _countPlayers;
        string mapName = currMaps[_currMapIndex].SceneName;
        SceneManager.LoadScene(mapName);
    }

    public void StartMiniGame()
    {
        _distanceMiniGameController.PlayerCarPrefab = _aviableCars[_currCarIndex].PlayerCarPrefab;
        string mapName = currMaps[_currMapIndex].SceneName;
        SceneManager.LoadScene(mapName);
    }

    private void SetCountPlayers()
    {
        _countPlayersText.text = _countPlayers.ToString();
        if(_currentGameMode==GameMode.Elimination)
            _countLaps = _countPlayers;
    } 
    
    private void SetCountLaps() => _countLapsText.text = _countLaps.ToString();

    public void HideGameModes() => _gameModes.SetActive(false);

    public void ShowMiniGames() => _miniGames.SetActive(true);
    
    public void HideMiniGames() => _miniGames.SetActive(false);
    
    public void SetRingGameMode() => _currentGameMode = GameMode.Ring;

    public void SetSprintGameMode()
    {
        _currentGameMode = GameMode.Sprint;
        _countLaps = 1;
    }

    public void SetEliminationGameMode() => _currentGameMode = GameMode.Elimination;

    public void SetDistanceMiniGame() => _currentGameMode = GameMode.DistanceMiniGame;

    public void Exit() => SceneManager.LoadScene("MainMenu");
}
