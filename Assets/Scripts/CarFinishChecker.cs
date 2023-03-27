using System.Collections;
using UnityEngine;


public class CarFinishChecker : MonoBehaviour
{
    public Car CarComponent;
    public bool IsFinishIntesected;
    private float _minTime = 30;
    private float _timer;

    public IEnumerator FinishIntersectTimer()
    {
        IsFinishIntesected = true;
        while (IsFinishIntesected)
        {
            _timer += 1;
            if (_timer >= _minTime)
            {
                IsFinishIntesected = false;
                _timer = 0;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
