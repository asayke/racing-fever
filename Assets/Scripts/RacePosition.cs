using System;
using UnityEngine;


public class RacePosition : MonoBehaviour, IComparable
{
    [HideInInspector] public int nextPoint;
    [HideInInspector] public float distToPoint;
    [HideInInspector] public int countLaps;
    
    public int CompareTo(object obj)
    {
        if (obj is RacePosition)
        {
            RacePosition racePosition = (RacePosition) obj;
            if (countLaps != racePosition.countLaps)
                return racePosition.countLaps - countLaps;
            else
            {
                if (nextPoint != racePosition.nextPoint)
                    return racePosition.nextPoint - nextPoint;
                else
                    return Mathf.CeilToInt(distToPoint - racePosition.distToPoint);

            }
        }

        return 0;
    }
}
