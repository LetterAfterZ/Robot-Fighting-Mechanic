using UnityEngine;

public class SuccessOutcomeGetItem : MonoBehaviour, ISuccessOutcome
{
    [SerializeField] Item _item = null;
    [SerializeField] PlayerHeldItem _playerHeldItem = null;
    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] SoundRandomiser _pickupSounds = null;

    public void TriggerSuccessAction()
    {
        _audioSource.PlayOneShot(_pickupSounds.ReturnRandomSound());
        _playerHeldItem.GetItem(_item);
    }
}
