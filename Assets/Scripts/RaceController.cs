using System;
using System.Collections.Generic;
using UnityEngine;


public class RaceController : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private RaceInfoUI _raceInfoUI;
    
    [HideInInspector] public int CountLaps;
    [HideInInspector] public GameMode GameMode;

    private List<Car> _cars = new List<Car>();
    private List<RacePosition> _racePositions = new List<RacePosition>();

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
        car.CarLapInfo.LapTimes.Add(car.CarLapTimer.LapTime);
        car.CarLapTimer.ResetLapTime();
        print("lap info: "+car.CarLapInfo.CountLaps+" "+car.CarLapInfo.LapTimes[^1]);
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

    private void Update()
    {
        if(!isRaceStarted)
            return;
        _racePositions.Sort();
        _raceInfoUI.RacePositionText.text =
            "Позиция: " + (_racePositions.IndexOf(_playerCar.RacePosition) + 1) + "/" + _racePositions.Count;
        _raceInfoUI.LapsText.text = "Круг: " + (_playerCar.CarLapInfo.CountLaps + 1) + "/" + CountLaps;
        _raceInfoUI.TimeText.text = "Время: "+_playerCar.CarLapTimer.AllTime;
    }
}
