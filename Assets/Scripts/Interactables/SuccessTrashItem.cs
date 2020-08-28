using System.Collections;
using UnityEngine;

public class SuccessTrashItem : MonoBehaviour, ISuccessOutcome
{
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] SoundRandomiser _binSounds = null;

    public void TriggerSuccessAction()
    {           
        StartCoroutine(TrashInBin());
    }

    private IEnumerator TrashInBin(){
        _playerHeldItem.TrashItem();  
        //play bin animation
        _audioSource.PlayOneShot(_binSounds.ReturnRandomSound());
        _animator.SetBool("IsTrashing", true);
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool("IsTrashing", false);        
    }
}