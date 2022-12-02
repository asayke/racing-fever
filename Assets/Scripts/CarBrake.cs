using System;
using System.Collections;
using UnityEngine;

public class CarBrake : MonoBehaviour
{
    [SerializeField] private LayerMask _barrierMask;
    private CarAI _carAI;
    private float _distanceToSteer = 30;

    private bool _isBraking;
    

    private void Awake()
    {
        _carAI = GetComponent<CarAI>();
        
    }

    private void Update()
    {
        Debug.DrawRay(_carAI.carFront.position, _carAI.transform.forward*30f);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(_carAI.carFront.position, _carAI.transform.forward, out hit,30, _barrierMask) && !_isBraking)
        {
            _isBraking = true;
            StartCoroutine(Brake());
        }
    }

    private IEnumerator Brake()
    {
        float firstSpeed = _carAI.frontLeft.rpm;
        RaycastHit hit = new RaycastHit();
        print("first speed "+firstSpeed);
        while (_carAI.frontLeft.rpm > firstSpeed*0.5f && Physics.Raycast(_carAI.carFront.position, _carAI.transform.forward, out hit,30, _barrierMask))
        {
            print("curr speed "+_carAI.frontLeft.rpm);
            float dist = Vector3.Distance(hit.point, _carAI.carFront.position);
            print("dist "+dist);
            if(dist > 25) 
                _carAI.MovementTorque -= 0.1f;
            else if(dist > 20) 
                _carAI.MovementTorque -= 0.08f;
            else if(dist > 15) 
                _carAI.MovementTorque -= 0.06f;
            else if(dist > 10) 
                _carAI.MovementTorque -= 0.04f;
            else if(dist > 5) 
                _carAI.MovementTorque -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }

        _carAI.MovementTorque = 1;
        _isBraking = false;
    }
}
