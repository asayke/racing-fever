using System;
using UnityEngine;

public static class Extensions
{
    public static string GetTimeString(this float timeInSec)
    {
        int minutes = (int) (timeInSec / 60);
        int seconds = (int) (timeInSec % 60);
        int ms = (int) ((timeInSec - Math.Truncate(timeInSec)) * 1000);
        TimeSpan timeSpan = new TimeSpan(0, 0, minutes, seconds, ms);
        return timeSpan.ToString(@"m\:ss\:ff");
    }
}