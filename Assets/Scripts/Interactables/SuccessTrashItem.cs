using System.Collections;
using UnityEngine;

public class SuccessTrashItem : MonoBehaviour, ISuccessOutcome
{
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;
    [SerializeField] private Animator _animator = null;

    public void TriggerSuccessAction()
    {           
        StartCoroutine(TrashInBin());
    }

    private IEnumerator TrashInBin(){
        _playerHeldItem.TrashItem();  
        //play bin animation
        _animator.SetBool("IsTrashing", true);
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool("IsTrashing", false);        
    }
}