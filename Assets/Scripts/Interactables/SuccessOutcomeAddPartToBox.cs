using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessOutcomeAddPartToBox : MonoBehaviour, ISuccessOutcome
{
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;
    [SerializeField] private BoxContainer box;
    
    [SerializeField] SpriteRenderer _keyPressDialog;

    private bool listenForKey = false;
    private Item _item;

    
    public void TriggerSuccessAction()
    {        
        // show key press
        _keyPressDialog.enabled = true;

        // add listener for keypress
        listenForKey = true;
        StartCoroutine(listenForKeyPress());
                
    }

    private IEnumerator listenForKeyPress()
    {
        while (listenForKey){            
            if(Input.GetKeyDown("space")) {                
                _playerHeldItem.ConsumeItem(out _item);
                box.PlayerAddItemToBox(_item);
                TerminateThisScript();
            }else{
                yield return null;
            }
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {     
        if (col.gameObject.tag == "Player")        
            TerminateThisScript();        
    }

    void TerminateThisScript(){
        //hide key press
        _keyPressDialog.enabled = false;    
        //terminate the listener
        listenForKey = false;
        
    }
}
