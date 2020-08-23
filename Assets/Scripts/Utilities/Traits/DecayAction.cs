using UnityEngine;

public class DecayAction : MonoBehaviour, IDecayAction
{
    [SerializeField] protected GameEvent _onDecayEvent = null;
    
    public void Decay(){
         _onDecayEvent.Raise(); //trigger callback event
        Object.Destroy(gameObject); //destroy this game object
    }
}
