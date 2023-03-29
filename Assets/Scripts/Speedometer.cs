using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    private PrometeoCarController _carController;
    private Text _text;

    private void Start()
    {
        _carController = FindObjectOfType<PrometeoCarController>();
        _text = GetComponent<Text>();
    } 

    private void Update() => _text.text = $"Скорость: {Math.Abs((int)_carController.carSpeed)} км/ч";
}
