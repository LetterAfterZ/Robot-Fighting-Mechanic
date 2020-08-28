using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{    

    private ITriggerCondition _triggerCondition = null;
    private ISuccessOutcome _successOutcome = null;
    
    [SerializeField] private bool _requireKeyPress = false;
    [SerializeField] SpriteRenderer _keyPressDialog = null; //the popup for what key to press - should be a nested child to the interactable
    private bool _listenForKey = false;
    private bool _abortLaunch = false;
    
    [SerializeField] private bool _requireMinigame = false;
    [SerializeField] private Minigame _minigame = null;
    private bool _IsActiveInteractable = false;
    private float _minigameYPosOffset = 1f;
        

    private void Awake()
    {
        _successOutcome = GetComponent<ISuccessOutcome>();
        _triggerCondition = GetComponent<ITriggerCondition>();
    }

    // This version lets you walk over trigger (need to tick Is Trigger on Collider2D)
    void OnTriggerEnter2D (Collider2D col)
    {     
        if (col.gameObject.tag == "Player")
            StartCoroutine(LaunchMinigame(col.gameObject.GetComponent<Transform>().position + new Vector3(0, _minigameYPosOffset,0)));
    }
    
    // This version won't let you move into the object (don't choose isTrigger)
    void OnCollisionEnter2D(Collision2D col)
    {                  
       if (col.gameObject.tag == "Player")
            StartCoroutine(LaunchMinigame(col.gameObject.GetComponent<Transform>().position + new Vector3(0, _minigameYPosOffset,0)));
    }

    IEnumerator LaunchMinigame(Vector2 spawnPosition)
    {
        //check Trigger condition is met
        if (_triggerCondition != null && _triggerCondition.TriggerConditionCheck()){
            
            //if requires key press
            if (_requireKeyPress){
                // show key popup
                _keyPressDialog.enabled = true;
                // add listener for keypress
                _listenForKey = true;
                _abortLaunch = false;
                while (_listenForKey){            
                    if(Input.GetKeyDown("space")) {
                        StopKeyPressCheck(true);
                    } else {
                       yield return null;
                    }
                }
            }

            if (_abortLaunch){
                yield break;
            }

            if (!_requireMinigame){ //bypass minigame if not set to required
                _IsActiveInteractable = true;
                OnSuccess();
                yield break;
            }
        
            GameObject minigameExists = GameObject.FindWithTag("Minigame");

            //if player enters the shelf - trigger the minigame, if one doesn't already exist 
            if (minigameExists == null){
                _IsActiveInteractable = true;
                Instantiate(_minigame, spawnPosition, Quaternion.identity);
            }
        }        
    }

    public void OnSuccess(){
        //NOTE: DON'T FORGET TO ADD THE EVENT LISTENER IF YOU HAVE A MINIGAME!
        if(_IsActiveInteractable){
            _successOutcome.TriggerSuccessAction();
            _IsActiveInteractable = false;
        }
    }

    void StopKeyPressCheck(bool playerTriggered){
        //hide key press
        _keyPressDialog.enabled = false;        
        //terminate the listener
        _listenForKey = false;   
                
        if (!playerTriggered) //if triggered by player leaving, don't proceed in script
            _abortLaunch = true;
    }

    void OnTriggerExit2D (Collider2D col)
    {     
        if (col.gameObject.tag == "Player" && _listenForKey)        
            StopKeyPressCheck(false);        
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && _listenForKey)        
            StopKeyPressCheck(false);   
    }

}
