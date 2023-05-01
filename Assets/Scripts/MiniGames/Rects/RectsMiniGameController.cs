using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RectsMiniGameController : MonoBehaviour
{
    private int _attempts = 1;
    private int _maxAttempts = 3;

    private int _score;
    
    public GameObject PlayerCarPrefab;

    private GameObject playerCar;
    private Rigidbody _playerCarRigidbody;
    
    private Countdown _countdown;
    private CameraSwitcher _cameraSwitcher;
    private CarStartPosition _carStartPosition;
    
    private RectsMiniGameUI _ui;
    private MiniGameEndScreen _endScreen;
    private DistanceArea _gameArea;
    private bool isEnteredArea;
    private bool isEarned;

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
        if(scene.name!="MiniGame2")
            return;
        print("start game");
        _countdown = GetComponent<Countdown>();
        _cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        _carStartPosition = FindObjectOfType<CarStartPosition>();
        _gameArea = FindObjectOfType<DistanceArea>();
        _ui = FindObjectOfType<RectsMiniGameUI>();
        _endScreen = FindObjectOfType<MiniGameEndScreen>();
        
        _endScreen.Restart.onClick.RemoveAllListeners();
        _endScreen.Restart.onClick.AddListener(Restart);
        _endScreen.Exit.onClick.RemoveAllListeners();
        _endScreen.Exit.onClick.AddListener(Exit);
        
        _gameArea.OnAreaEnter += DisableMove;
        _gameArea.OnAreaExit += EnableDrag;
        
        playerCar = Instantiate(PlayerCarPrefab, _carStartPosition.transform.position, Quaternion.identity);
        _playerCarRigidbody = playerCar.GetComponent<Rigidbody>();
        playerCar.transform.Rotate(new Vector3(0,10,0));
        foreach (var cam in _cameraSwitcher.Cams)
        {
            cam.Follow = playerCar.transform;
            cam.LookAt = playerCar.transform;
        }
        
        FindRectTriggers();
        _countdown.StartCountdown();
    }

    private void FindRectTriggers()
    {
        foreach (var rectTrigger in FindObjectsOfType<RectTrigger>())
        {
            rectTrigger.OnRectEnter += EarnScore;
        }
    }

    private void EarnScore(int score)
    {
        if(isEarned)
            return;
        isEarned = true;
        _score += score;
        _ui.Score.text = "Очков: " + _score;
        print("Score: "+_score);
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
        if (isEnteredArea && _playerCarRigidbody.velocity.sqrMagnitude <= Mathf.Epsilon)
        {
            EndAttempt();
        }
    }
    
    private void Restart()
    {
        _attempts = 1;
        _score = 0;
        _ui.Attempts.text = "Попытка: " + (_attempts) + "/" + _maxAttempts;
        _ui.Score.text = "Очков: " + _score;
        
        _endScreen.container.SetActive(false);
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
    }

    private void EndAttempt()
    {
        isEnteredArea = false;
        isEarned = false;
        print(_attempts+" "+_maxAttempts);
        if (_attempts + 1 > _maxAttempts)
        {
            EndGame();
            return;
        }
        _attempts++;
        _ui.Attempts.text = "Попытка: " + (_attempts) + "/" + _maxAttempts;
        
        _endScreen.container.SetActive(false);
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
    }
    
    private void EndGame()
    {
        Time.timeScale = 0;
        _endScreen.container.SetActive(true);
        _endScreen.Result.text = "Ваш результат: " + _score + " очков!";
    }
    
    private void Exit()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
