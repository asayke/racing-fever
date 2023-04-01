using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RacePositionTracker : MonoBehaviour
{
    public List<Transform> _wayPoints;
    private int _currPointIndex = 0;
    private RacePosition _racePosition;
    private float _wayPointDetectDist = 100;
    private Car _car;

    private void Awake()
    {
        _racePosition = GetComponent<RacePosition>();
        _car = GetComponent<Car>();
        _wayPoints = FindObjectOfType<WaypointCircuit>().Waypoints.ToList();
        _racePosition.distToPoint = GetDistToPoint();
    }

    private void Update()
    {
        if (GetDistToPoint() <= _wayPointDetectDist)
        {
            _currPointIndex = (_currPointIndex+1) % _wayPoints.Count;
        }
        
        _racePosition.nextPoint = _currPointIndex;
        _racePosition.countLaps = _car.CarLapInfo.CountLaps;

        _racePosition.distToPoint = GetDistToPoint();
    }

    private float GetDistToPoint()
    {
        Vector3 currPos = transform.position;
        Vector3 pointPos = _wayPoints[_currPointIndex].position;
        return Mathf.Pow(currPos.x - pointPos.x,2) + Mathf.Pow(currPos.y - pointPos.y,2) +
               Mathf.Pow(currPos.z - pointPos.z,2);
    }
}
