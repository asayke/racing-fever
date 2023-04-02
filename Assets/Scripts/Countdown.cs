using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int currSec = 3;
    private ArcadeAiVehicleController[] _aiCars;
    private RaceController _raceController;
    

    public void StartCountdown()
    {
        currSec = 3;
        Time.timeScale = 1;
        _text.enabled = true;
        _aiCars = FindObjectsOfType<ArcadeAiVehicleController>();
        _raceController = FindObjectOfType<RaceController>();
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
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
            print(_aiCars.Length);
            foreach (var aiCar in _aiCars)
            {
                try
                {
                    aiCar.enabled = true;
                }
                catch (MissingReferenceException e)
                {
                    
                }
            }
            if(_raceController!=null)
                _raceController.StartRace();
        }
        
    }
}
