using UnityEngine;


public class CarLapTimer : MonoBehaviour
{
    public float Timer;
    private void Update() => Timer += Time.deltaTime;

    public void ResetTimer() => Timer = 0;
}
