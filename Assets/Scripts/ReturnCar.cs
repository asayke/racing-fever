using System;
using UnityEngine;


public class ReturnCar : MonoBehaviour
{
    private RacePositionTracker _racePositionTracker;
    private void Awake()
    {
        _racePositionTracker = GetComponent<RacePositionTracker>();
    }

    private void Update()
    {
        if (SimpleInput.GetKeyDown(KeyCode.R))
        {
            transform.position = _racePositionTracker.GetLastPoint();
        }
    }
}
