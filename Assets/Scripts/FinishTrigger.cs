using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _carMask;
    public event Action<CarFinishChecker> OnFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.IsInLayerMask(_carMask))
        {
            CarFinishChecker car = other.gameObject.GetComponent<CarFinishChecker>();
            if(car.IsFinishIntesected)
                return;
            OnFinished?.Invoke(car);
        }
    }
}
