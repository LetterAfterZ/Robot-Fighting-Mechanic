using UnityEngine;

public class TriggerConditionItemCheck : MonoBehaviour, ITriggerCondition
{
    [SerializeField] bool checkHasNoItem = false;    
    [SerializeField] bool checkHasItem = false;
    
    //[SerializeField] bool checkHasSpecificItem = false;
    //[SerializeField] Item specificItem = null;
    
    public bool TriggerConditionCheck(){
        bool hasItem = PlayerHeldItem.hasItem;
        if(checkHasNoItem && !hasItem)
            return true;

        if(checkHasItem && hasItem)
            return true;

        /*
        if(checkHasSpecificItem && hasItem){
            Item item  = PlayerHeldItem.heldItem;
            if (item.itemID == specificItem.itemID);
                return true;
        } 
        */
    
        return false;
    }
}
