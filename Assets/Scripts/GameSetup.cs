﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSetup : MonoBehaviour
{
    [HideInInspector] public GameMode GameMode;
    [HideInInspector] public GameObject PlayerCarPrefab;
    [HideInInspector] public int CountPlayers;
    [HideInInspector] public int CountLaps;

    [SerializeField] private CarDatabase _carDatabase;
    [SerializeField] private List<Transform> _carPositions;
    [SerializeField] private CameraSwitcher _cameraSwitcher;

    private GameObject playerCar;
    private Countdown _countdown;
    private RaceController _raceController;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartSetup;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartSetup;
    }

    private void GetCarPositions()
    {
        _carPositions = FindObjectsOfType<CarStartPosition>().Select(x => x.transform).ToList();
    }

    private void StartSetup(Scene scene, LoadSceneMode loadMode)
    {
        if(scene.name=="SelectMode")
            return;
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
            return;
        }
            
        
        print("setup");
        _countdown = GetComponent<Countdown>();
        _raceController = FindObjectOfType<RaceController>();
        _cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        
        GetCarPositions();
        playerCar = Instantiate(PlayerCarPrefab, _carPositions[0].position, Quaternion.identity);
        playerCar.transform.Rotate(new Vector3(0,90,0));
        foreach (var cam in _cameraSwitcher.Cams)
        {
            cam.Follow = playerCar.transform;
            cam.LookAt = playerCar.transform;
        }
        _raceController.RegisterCar(playerCar.GetComponent<Car>());

        for (int i = 1; i <= CountPlayers; i++)
        {
            var enemy = Instantiate(_carDatabase.cars[i - 1].EnemyCarPrefab,_carPositions[i].position, Quaternion.identity);
            enemy.transform.Rotate(new Vector3(0,90,0));
            _raceController.RegisterCar(enemy.GetComponent<Car>());
        }
        
        _raceController.CountLaps = CountLaps;
        _raceController.GameMode = GameMode;

        _countdown.StartCountdown();
    }

    public void Restart()
    {
        _raceController.Clear();
        
        playerCar = Instantiate(PlayerCarPrefab, _carPositions[0].position, Quaternion.identity);
        playerCar.transform.Rotate(new Vector3(0,90,0));
        foreach (var cam in _cameraSwitcher.Cams)
        {
            cam.Follow = playerCar.transform;
            cam.LookAt = playerCar.transform;
        }
        _raceController.RegisterCar(playerCar.GetComponent<Car>());

        for (int i = 1; i <= CountPlayers; i++)
        {
            var enemy = Instantiate(_carDatabase.cars[i - 1].EnemyCarPrefab,_carPositions[i].position, Quaternion.identity);
            enemy.transform.Rotate(new Vector3(0,90,0));
            _raceController.RegisterCar(enemy.GetComponent<Car>());
        }

        _countdown.StartCountdown();
    }
}
