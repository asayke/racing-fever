using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int currSec = 3;
    private CarAI[] _aiCars;

    private void Awake()
    {
        Time.timeScale = 1;
        _aiCars = FindObjectsOfType<CarAI>();
        foreach (var aiCar in _aiCars)
        {
            aiCar.IsBraking = true;
        }

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (currSec >= 0)
        {
            _text.SetText(currSec.ToString());
            yield return new WaitForSeconds(1);
            currSec--;
        }
        
        if (currSec == -1)
        {
            _text.enabled = false;
            FindObjectOfType<PrometeoCarController>().isMoveAllow = true;
            foreach (var aiCar in _aiCars) aiCar.IsBraking = false;
        }
        
    }
}
