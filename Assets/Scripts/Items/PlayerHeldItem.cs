using UnityEngine;

public class PlayerHeldItem : ScriptableObject
{
    [SerializeField] public static bool hasItem;
    [SerializeField] public static Item heldItem;
    [SerializeField] private GameEvent _inventoryUpdatedEvent  = null;

    public bool GetItem(Item item){
        if (!hasItem){
            heldItem = item;
            hasItem = true;
            _inventoryUpdatedEvent.Raise();
            return true;
        }
        return false;
    }

    public bool ConsumeItem(out Item item){
        item = null;
        if (hasItem){
            hasItem = false;
            item = heldItem;
            _inventoryUpdatedEvent.Raise();
            return true;
        }
        return false;
    }

    public bool ReturnCurrentItem(out Item item){
        item = null;
        if (hasItem){
            item = heldItem;
            return true;
        }
        return false;
    }

    public void TrashItem(){
        hasItem = false;
        heldItem = null;
        _inventoryUpdatedEvent.Raise();
    }

    public bool HasItem(){
        return heldItem;
    }
}