using UnityEngine;


public class CarLapTimer : MonoBehaviour
{
    public float AllTime;
    public float LapTime;

    private void Update()
    {
        AllTime += Time.deltaTime;
        LapTime += Time.deltaTime;
    }

    public void ResetLapTime() => LapTime = 0;

    public void ResetAllTime()
    {
        AllTime = 0;
        LapTime = 0;
    }
}
