using System;
using System.Collections.Generic;
using UnityEngine;


public class RaceController : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private RaceInfoUI _raceInfoUI;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private EliminationEndScreen _eliminationEndScreen;
     
    [HideInInspector] public int CountLaps;
    [HideInInspector] public GameMode GameMode;

    private List<Car> _cars = new List<Car>();
    private List<RacePosition> _racePositions = new List<RacePosition>();
    private List<Car> _finishedCars = new List<Car>();

    private Car _playerCar;

    private bool isRaceStarted;
 
    private void OnEnable() => _finishTrigger.OnFinished += OnFinish;

    private void OnDisable() => _finishTrigger.OnFinished -= OnFinish;

    private void OnFinish(CarFinishChecker car)
    {
        Debug.Log("on finish");
        StartCoroutine(car.FinishIntersectTimer());
        if (car.CarComponent.isFirstLap)
        {
            car.CarComponent.isFirstLap = false;
            return;
        }
        CountLap(car.CarComponent);
    }

    private void CountLap(Car car)
    {
        car.CarLapInfo.CountLaps++;
        car.RacePosition.countLaps = car.CarLapInfo.CountLaps;
        _racePositions.Sort();
        car.CarLapInfo.LapTimes.Add(car.CarLapTimer.LapTime);
        car.CarLapTimer.ResetLapTime();
        if (GameMode == GameMode.Elimination)
        {
            HandleElimination(car);
            return;
        }
        if(car.CarLapInfo.CountLaps==CountLaps)
            FinishCar(car);
        print("lap info: "+car.CarLapInfo.CountLaps+" "+car.CarLapInfo.LapTimes[^1]);
    }

    private void HandleElimination(Car car)
    {
        print("index "+_racePositions.IndexOf(car.RacePosition)+" "+car.Name);
        if (_racePositions.IndexOf(car.RacePosition) == (_racePositions.Count - 1))
        {
            if (car is PrometeoCarController)
            {
                EndElimination(false, car.CarLapInfo.CountLaps);
                DeleteCarFromRace(car);
                return;
            }
            DeleteCarFromRace(car);
            if (_cars.Count == 2 && _racePositions.IndexOf(_playerCar.RacePosition) == 0)
            {
                EndElimination(true, _playerCar.CarLapInfo.CountLaps);
            }
        }
    }

    public void RegisterCar(Car car)
    {
        _cars.Add(car);
        _racePositions.Add(car.RacePosition);
        if (car is PrometeoCarController)
        {
            _playerCar = car;
        }
    }

    public void StartRace()
    {
        foreach (var car in _cars)
        {
            car.CarLapTimer.ResetAllTime();
        }
        isRaceStarted = true;
    }

    private void FinishCar(Car car)
    {
        _finishedCars.Add(car);
        if (car is ArcadeAiVehicleController carAi)
        {
            carAi.enabled = false;
        }
        else if (car is PrometeoCarController playerCar)
        {
            playerCar.isMoveAllow = false;
            FinishRace();
        }
    }

    private void EndElimination(bool isWinner, int countLaps)
    {
        Time.timeScale = 0;
        _eliminationEndScreen.gameObject.SetActive(true);
        _eliminationEndScreen.FillScreen(isWinner,countLaps);
    }

    private void FinishRace()
    {
        Time.timeScale = 0;
        _endScreen.gameObject.SetActive(true);
        _endScreen.FillScreen(_finishedCars);
    }

    private void DeleteCarFromRace(Car car)
    {
        _cars.Remove(car);
        _racePositions.Remove(car.RacePosition);
        Destroy(car.gameObject);
    }

    private void Update()
    {
        if(!isRaceStarted)
            return;
        _racePositions.Sort();
        _raceInfoUI.RacePositionText.text =
            "Позиция: " + (_racePositions.IndexOf(_playerCar.RacePosition) + 1) + "/" + _racePositions.Count;
        _raceInfoUI.LapsText.text = "Круг: " + (_playerCar.CarLapInfo.CountLaps + 1) + "/" + CountLaps;
        _raceInfoUI.TimeText.text = "Время: "+_playerCar.CarLapTimer.AllTime.GetTimeString();
    }

    public void Clear()
    {
        foreach (var car in _cars)
        {
            Destroy(car.gameObject);
        }
        _cars.Clear();
        _racePositions.Clear();
        _finishedCars.Clear();
        isRaceStarted = false;
    }
}
