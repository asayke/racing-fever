using System;
using System.Collections.Generic;
using UnityEngine;


public class RaceController : MonoBehaviour
{
    [SerializeField] private List<Car> _cars;
    [SerializeField] private int _countLaps;

    [SerializeField] private FinishTrigger _finishTrigger;

    private void OnEnable()
    {
        _finishTrigger.OnFinished += OnFinish;
    }

    private void OnDisable()
    {
        _finishTrigger.OnFinished -= OnFinish;
    }

    private void OnFinish()
    {
        Debug.Log("on finish");
    }
}
