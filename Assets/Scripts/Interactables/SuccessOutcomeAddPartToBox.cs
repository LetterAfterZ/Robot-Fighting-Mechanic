using System.Collections;
using UnityEngine;

public class SuccessOutcomeAddPartToBox : MonoBehaviour, ISuccessOutcome
{
    //Box specific 
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;
    [SerializeField] private BoxContainer box = null;
    private Item _item = null;
    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] SoundRandomiser _installSounds = null;

   
    [SerializeField] SpriteRenderer _keyPressDialog = null;
    private bool listenForKey = false;
    
    public void TriggerSuccessAction()
    {        
        // show key popup
        _keyPressDialog.enabled = true;

        // add listener for keypress
        listenForKey = true;
        StartCoroutine(listenForKeyPress());                
    }

    private IEnumerator listenForKeyPress()
    {
        while (listenForKey){            
            if(Input.GetKeyDown("space")) {
                TriggerFinalAction();
                TerminateThisScript();
            } else {
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

    private void TriggerFinalAction() {
        _audioSource.PlayOneShot(_installSounds.ReturnRandomSound());
        _playerHeldItem.ConsumeItem(out _item);
        box.PlayerAddItemToBox(_item);
    }
}
