using System.Collections;
using UnityEngine;

public class EvalRepairs : MonoBehaviour
{
    public BoxContainer[] boxes;

    [SerializeField] SpriteRenderer _blueBoxSR = null;
    [SerializeField] SpriteRenderer _redBoxSR = null;
    [SerializeField] SpriteRenderer _greenBoxSR = null;
    [SerializeField] BoxManager _boxManager = null;

    [SerializeField] RobotHealth _robotHealth = null;
    [SerializeField] RobotLaser _robotLaser = null;
    [SerializeField] int _laserChargeRate = 15;
    [SerializeField] int _partDamageRate = 5;

    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] AudioClip _successSound = null;
    [SerializeField] AudioClip _failureSound = null;

    static bool firstRound = true;

    public void EvaluateRepairs()
    {
        if (firstRound){ //don't eval on the first round.
            firstRound = false;
            return;
        }

        //Remove matching parts
        foreach (BoxContainer box in boxes)
        {
            if (box.boxRequiredParts.Count > 0){
                for (int i=box.boxRequiredParts.Count -1; i >= 0; i--)
                {
                    if (box.boxAddedParts.Count > 0){
                        for (int j=box.boxAddedParts.Count -1; j >= 0; j--)
                        {
                            //if they match remove them both & increment laser progress
                            if (box.boxRequiredParts[i] == box.boxAddedParts[j])
                            {
                                Debug.Log("Success - part removed");
                                box.boxRequiredParts.RemoveAt(i);
                                box.boxAddedParts.RemoveAt(j);
                                //increment laser
                                _robotLaser.ChargeLaser(_laserChargeRate);
                                break; //exit the loop if matched
                            }
                        }
                    }
                }
            }
        }

        bool allCorrect = true;
        // Apply damage for missed or extra parts
        foreach (BoxContainer box in boxes)
        {
            bool success = true;
            //damage for each missed part
            if (box.boxRequiredParts.Count > 0){
                Debug.Log("You missed " + box.boxRequiredParts.Count + " part(s)!");
                _robotHealth.TakeDamage(_partDamageRate * box.boxRequiredParts.Count);
                success = false;
                allCorrect = false;
            }
            if (box.boxAddedParts.Count > 0){
                Debug.Log("You added " + box.boxAddedParts.Count + " unneeded part(s)!");
                _robotHealth.TakeDamage(_partDamageRate * box.boxAddedParts.Count);
                success = false;
                allCorrect = false;
            }            
            StartCoroutine(FlashBox(box, success));
        }

        //play sound based on overall results
        if (allCorrect){
            _audioSource.PlayOneShot(_successSound);
        } else {
            _audioSource.PlayOneShot(_failureSound);
        }
    }

    private IEnumerator FlashBox(BoxContainer box, bool success){
        Color color = Color.red;
        if(success){
             color = Color.green;
             yield return null;
        }

        //get box
        SpriteRenderer boxSR = null;
        switch(box.boxID)
        {
            case 0:
                boxSR = _blueBoxSR;
                break;
            case 1:
                boxSR = _redBoxSR;
                break;
            case 2:
                boxSR = _greenBoxSR;
                break;
            default:
                break;
        }
        int flashTimer = 6;        
        
        //flash the box
        while(flashTimer > 0){            
            boxSR.color = color;
            yield return new WaitForSeconds(0.2f);
            boxSR.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            flashTimer--;    
            yield return null;
        }

        //trigger disabling boxes
        _boxManager.TriggerDisableBoxes();
    }
}
