using System;
using UnityEngine;


public class RectTrigger : MonoBehaviour
{
    public event Action<int> OnRectEnter;
    
    [SerializeField] private int _score;
    
    private void OnTriggerEnter(Collider other)
    {
        OnRectEnter?.Invoke(_score);
    }
}
