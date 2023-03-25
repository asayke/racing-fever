using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _carMask;
    public event Action OnFinished;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.IsInLayerMask(_carMask))
        {
            OnFinished?.Invoke();
        }
    }
}
