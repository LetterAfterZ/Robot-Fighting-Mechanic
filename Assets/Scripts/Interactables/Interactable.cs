﻿using UnityEngine;

public class Interactable : MonoBehaviour
{    

    private ITriggerCondition _triggerCondition = null;
    private ISuccessOutcome _successOutcome = null;
    [SerializeField] private bool _requireMinigame = false;
    [SerializeField] private Minigame _minigame = null;
    private bool _IsActiveInteractable = false;

    private void Awake() {
        _successOutcome = GetComponent<ISuccessOutcome>();
        _triggerCondition = GetComponent<ITriggerCondition>();
    }

    // This version lets you walk over trigger (need to tick Is Trigger on Collider2D)
    void OnTriggerEnter2D (Collider2D col){     
        if (col.gameObject.tag == "Player")
            LaunchMinigame();
    }
    
    // This version won't let you move into the object (don't choose isTrigger)
    void OnCollisionEnter2D(Collision2D col){                  
       if (col.gameObject.tag == "Player")
            LaunchMinigame();
    }

    void LaunchMinigame(){
        //check Trigger condition is met
        if (_triggerCondition != null && _triggerCondition.TriggerConditionCheck()){
            if (!_requireMinigame){ //bypass minigame if not set to required
                _IsActiveInteractable = true;
                OnSuccess();
                return;
            }
        
            GameObject minigameExists = GameObject.FindWithTag("Minigame");

            //if player enters the shelf - trigger the minigame, if one doesn't already exist 
            if (minigameExists == null){
                _IsActiveInteractable = true;
                Instantiate(_minigame, new Vector2(0,0), Quaternion.identity);
            }
        }        
    }

    public void OnSuccess(){
        //NOTE: DON'T FORGET TO ADD THE EVENT LISTENER!
        if(_IsActiveInteractable){
            _successOutcome.TriggerSuccessAction();
            _IsActiveInteractable = false;
        }
    }

}
