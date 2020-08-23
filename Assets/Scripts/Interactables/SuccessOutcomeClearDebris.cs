using UnityEngine;

public class SuccessOutcomeClearDebris : MonoBehaviour, ISuccessOutcome
{
    public void TriggerSuccessAction()
    {
        Destroy(gameObject);
    }
}
