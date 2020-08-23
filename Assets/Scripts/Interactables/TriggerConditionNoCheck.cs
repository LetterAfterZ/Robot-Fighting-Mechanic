using UnityEngine;

public class TriggerConditionNoCheck : MonoBehaviour, ITriggerCondition
{
    public bool TriggerConditionCheck(){
        return true;
    }
}
