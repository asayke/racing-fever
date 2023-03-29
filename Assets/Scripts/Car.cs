using UnityEngine;

public class Car : MonoBehaviour
{
    public CarLapInfo CarLapInfo = new CarLapInfo();
    public CarLapTimer CarLapTimer;
    public RacePosition RacePosition;
    public bool isFirstLap = true;
}
