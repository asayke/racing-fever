using System.Collections.Generic;
using UnityEngine;


public class RaceController : MonoBehaviour
{
    [SerializeField] private List<Car> _cars;
    [SerializeField] private int _countLaps;

    [SerializeField] private FinishTrigger _finishTrigger;

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
        car.CarLapInfo.LapTimes.Add(car.CarLapTimer.Timer);
        car.CarLapTimer.ResetTimer();
        print("lap info: "+car.CarLapInfo.CountLaps+" "+car.CarLapInfo.LapTimes[^1]);
    }
}
