using UnityEngine;

public class SuccessOutcomeGetItem : MonoBehaviour, ISuccessOutcome
{
    [SerializeField] private Item _item = null;
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;

    public void TriggerSuccessAction()
    {
        _playerHeldItem.GetItem(_item);
    }
}
