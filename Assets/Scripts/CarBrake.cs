using System.Collections;
using UnityEngine;

public class CarBrake : MonoBehaviour
{
    [SerializeField] private LayerMask _barrierMask;
    private CarAI _carAI;
    private float _distanceToSteer = 30;

    private bool _isBraking;
    

    private void Awake() => _carAI = GetComponent<CarAI>();

    private void Update()
    {
        Debug.DrawRay(_carAI.carFront.position, _carAI.transform.forward*30f);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(_carAI.carFront.position, _carAI.transform.forward, out hit,100, _barrierMask) && !_isBraking && _carAI.frontLeft.rpm>500)
        {
            _isBraking = true;
            StartCoroutine(Brake(hit));
        }
    }

    private IEnumerator Brake(RaycastHit hit)
    {
        float firstSpeed = _carAI.frontLeft.rpm;
        float firstMotorTorque = _carAI.backRight.motorTorque;
        RaycastHit currHit = new RaycastHit();
        float firstDist = Vector3.Distance(hit.point, _carAI.carFront.position);
        while (_carAI.frontLeft.rpm > firstSpeed*0.5f && Physics.Raycast(_carAI.carFront.position, _carAI.transform.forward, out currHit,100, _barrierMask))
        {
            if(_carAI.BackMovement || _carAI.frontLeft.rpm<=500)
                break;
            float dist = Vector3.Distance(currHit.point, _carAI.carFront.position);
            float motorTorque = dist / firstDist * firstMotorTorque;
            _carAI.IsBraking = true;
            _carAI.backRight.motorTorque = motorTorque;
            _carAI.backLeft.motorTorque = motorTorque;
            _carAI.frontRight.motorTorque = motorTorque;
            _carAI.frontLeft.motorTorque = motorTorque;
            
            yield return new WaitForSeconds(0.01f);
        }

        _carAI.MovementTorque = 1;
        _isBraking = false;
        _carAI.IsBraking = false;
    }
}
