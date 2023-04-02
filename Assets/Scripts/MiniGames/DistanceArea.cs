using System;
using UnityEngine;


public class DistanceArea : MonoBehaviour
{
    public event Action OnAreaEnter;
    public event Action OnAreaExit;
    public BoxCollider Collider;
    public Transform StartPoint;

    private void OnTriggerEnter(Collider other)
    {
        print("trigger enter");
        OnAreaEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        print("trigger exit");
        OnAreaExit?.Invoke();
    }
}
