using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSetup : MonoBehaviour
{
    public GameMode GameMode;
    public GameObject PlayerCarPrefab;
    public int CountPlayers;
    public int CountLaps;

    [SerializeField] private CarDatabase _carDatabase;
    [SerializeField] private List<Transform> _carPositions;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    private GameObject playerCar;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartSetup;
    }

    private void StartSetup(Scene scene, LoadSceneMode loadMode)
    {
        playerCar = Instantiate(PlayerCarPrefab, _carPositions[0]);
        _playerCamera.Follow = playerCar.transform;
        _playerCamera.LookAt = playerCar.transform;

        for (int i = 0; i < CountPlayers; i++)
        {
            
        }
    }
}
