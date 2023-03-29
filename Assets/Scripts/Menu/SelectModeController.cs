﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectModeController : MonoBehaviour
{
    [SerializeField] private GameSetup _gameSetup;
    [SerializeField] private GameObject _gameModes;
    [SerializeField] private GameObject _mapSelector;
    [SerializeField] private GameObject _carSelector;
    [SerializeField] private GameObject _countPlayersSettings;
    [SerializeField] private GameObject _countLapsSettings;
    [SerializeField] private GameObject _playButton;

    [SerializeField] private Image _carImage;
    [SerializeField] private Text _carName;
    [SerializeField] private Image _mapImage;
    [SerializeField] private Text _mapName;

    [SerializeField] private Text _countPlayersText;
    [SerializeField] private Text _countLapsText;

    [SerializeField] private CarDatabase _carDatabase;
    [SerializeField] private MapDatabase _mapDatabase;

    [SerializeField] private int _maxCountPlayers;
    [SerializeField] private int _maxCountLaps;

    private int _currCarIndex;
    private MapMenuItem[] currMaps;
    private int _currMapIndex;
    private GameMode _currentGameMode;
    private int _countPlayers;
    private int _countLaps;

    public void ShowGameSettings()
    {
        _mapSelector.SetActive(true);
        _carSelector.SetActive(true);
        _playButton.SetActive(true);
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

    public void ShowCarItem()
    {
        CarMenuItem item = _carDatabase.cars[_currCarIndex];
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
        _currCarIndex = _currCarIndex % _carDatabase.cars.Length;
        ShowCarItem();
    }

    public void PrevCar()
    {
        _currCarIndex--;
        if (_currCarIndex < 0)
            _currCarIndex = _carDatabase.cars.Length - 1;
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
        if(_countPlayers>0)
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
        if(_countLaps>0)
            _countLaps--;
        SetCountLaps();
    }

    public void StartGame()
    {
        _gameSetup.GameMode = _currentGameMode;
        _gameSetup.PlayerCarPrefab = _carDatabase.cars[_currCarIndex].PlayerCarPrefab;
        _gameSetup.CountLaps = _countLaps;
        _gameSetup.CountPlayers = _countPlayers;
        string mapName = currMaps[_currMapIndex].SceneName;
        SceneManager.LoadScene(mapName);
    }

    private void SetCountPlayers() => _countPlayersText.text = _countPlayers.ToString();
    
    private void SetCountLaps() => _countLapsText.text = _countLaps.ToString();

    public void HideGameModes() => _gameModes.SetActive(false);
    
    public void SetRingGameMode() => _currentGameMode = GameMode.Ring;
    
    public void SetSprintGameMode() => _currentGameMode = GameMode.Sprint;
    
    public void SetEliminationGameMode() => _currentGameMode = GameMode.Elimination;

    public void Exit() => SceneManager.LoadScene("MainMenu");
}
