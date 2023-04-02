
using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceMiniGameController : MonoBehaviour
{
    public GameObject PlayerCarPrefab;

    private GameObject playerCar;
    private Rigidbody _playerCarRigidbody;
    
    private Countdown _countdown;
    private CameraSwitcher _cameraSwitcher;
    private CarStartPosition _carStartPosition;

    private DistanceArea _distanceArea;
    private bool isStarted;
    private bool isEnteredArea;

    private DistanceMiniGameUI _ui;
    private MiniGameEndScreen _endScreen;

    private float _dist;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartGame;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartGame;
    }

    private void StartGame(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.name!="MiniGame1")
            return;
        _countdown = GetComponent<Countdown>();
        _cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        _carStartPosition = FindObjectOfType<CarStartPosition>();
        _distanceArea = FindObjectOfType<DistanceArea>();
        _ui = FindObjectOfType<DistanceMiniGameUI>();
        _endScreen = FindObjectOfType<MiniGameEndScreen>();
        
        _endScreen.Restart.onClick.RemoveAllListeners();
        _endScreen.Restart.onClick.AddListener(Restart);
        _endScreen.Exit.onClick.RemoveAllListeners();
        _endScreen.Exit.onClick.AddListener(Exit);
        
        _distanceArea.OnAreaEnter += DisableMove;
        _distanceArea.OnAreaExit += EnableDrag;
        
        playerCar = Instantiate(PlayerCarPrefab, _carStartPosition.transform.position, Quaternion.identity);
        _playerCarRigidbody = playerCar.GetComponent<Rigidbody>();
        playerCar.transform.Rotate(new Vector3(0,10,0));
        foreach (var cam in _cameraSwitcher.Cams)
        {
            cam.Follow = playerCar.transform;
            cam.LookAt = playerCar.transform;
        }
        
        _countdown.StartCountdown();
        isStarted = true;
    }

    private void Exit()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    private void EnableDrag()
    {
        playerCar.GetComponent<PrometeoCarController>().Brakes();
    }


    private void DisableMove()
    {
        isEnteredArea = true;
        var controller = playerCar.GetComponent<PrometeoCarController>();
        controller.isMoveAllow = false;
    }

    private void Update()
    {
        if (isStarted)
        {
            _dist = playerCar.transform.position.z-_distanceArea.StartPoint.position.z;
            _dist = Mathf.Clamp(_dist, 0, _dist);
            _ui._distanceText.text = "Дистанция: "+(int)_dist + " метров";
        }

        if (isEnteredArea && _playerCarRigidbody.velocity.sqrMagnitude <= Mathf.Epsilon)
        {
            EndGame();
        }
    }

    private void Restart()
    {
        _endScreen.container.SetActive(false);
        isStarted = false;
        isEnteredArea = false;
        Destroy(playerCar.gameObject);
        playerCar = Instantiate(PlayerCarPrefab, _carStartPosition.transform.position, Quaternion.identity);
        _playerCarRigidbody = playerCar.GetComponent<Rigidbody>();
        playerCar.transform.Rotate(new Vector3(0,10,0));
        playerCar.GetComponent<PrometeoCarController>().isMoveAllow = false;
        foreach (var cam in _cameraSwitcher.Cams)
        {
            cam.Follow = playerCar.transform;
            cam.LookAt = playerCar.transform;
        }
        
        _countdown.StartCountdown();
        isStarted = true;
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        _endScreen.container.SetActive(true);
        _endScreen.Result.text = "Ваш результат: " + (int)_dist + " метров!";
    }
}
