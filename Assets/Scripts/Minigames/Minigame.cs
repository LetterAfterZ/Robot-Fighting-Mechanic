using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{    
    [SerializeField] protected RectTransform _parent = null;
    [SerializeField] protected float _timeBetweenFails = 1.5f;
    [SerializeField] protected float _failShakeTime = 1f;
    [SerializeField] protected float _shakeAmount = 5f;
    [SerializeField] protected GameEvent _minigameStartedEvent;
    [SerializeField] protected GameEvent _successEvent;

    protected bool _failTriggered = false;
    protected float _shakeCounter = 0;
    protected Vector2 _defaultPos;


    protected virtual void Start() {
        _minigameStartedEvent.Raise();
        //get initial UI position
        _defaultPos = _parent.position;
    }

    protected virtual void TriggerSuccess(){
        //Debug.Log("Success");
        _successEvent.Raise();
        Destroy(gameObject);

    }

    protected virtual void TriggerFailure(){
        //Debug.Log("Failure");
        _failTriggered = true;
    }

    protected IEnumerator FailShake(){
        while (_shakeCounter < _failShakeTime){ 
            _shakeCounter += Time.deltaTime; 
            //shake the interface
            _parent.position = _defaultPos + (Vector2)UnityEngine.Random.insideUnitSphere * _shakeAmount;
            yield return null;
        }
        _parent.position = _defaultPos; //Reset to original postion
    }


}
