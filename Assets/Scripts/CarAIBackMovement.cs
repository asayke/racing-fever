using System.Collections;
using UnityEngine;

public class CarAIBackMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _barrierLayerMask;
    private CarAI _carAI;
    private WaitForSeconds _waiting;
    private WaitForSeconds _backMoving;
    
    private void Awake()
    {
        _carAI = GetComponent<CarAI>();
        _waiting = new WaitForSeconds(2.3f);
        _backMoving = new WaitForSeconds(1.2f);
    }

    private IEnumerator ToBackMovement()
    {
        yield return _waiting;
        _carAI.BackMovement = true;
        _carAI.MovementTorque = -1;
        yield return _backMoving;
        _carAI.BackMovement = false;
        _carAI.MovementTorque = 1;
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.IsInLayerMask(_barrierLayerMask)) StartCoroutine(ToBackMovement());
    }
}