using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPartTracker : MonoBehaviour
{
    public BoxContainer blueBox;
    public BoxContainer redBox;
    public BoxContainer greenBox;    

    [SerializeField] private Item _itemBattery;
    [SerializeField] private Item _itemCircuitBoard;
    [SerializeField] private Item _itemGear;

    [SerializeField] private Bubble _bubble;

    private enum BoxColor {Red,Blue,Green}
    private enum ItemPart {Battery,CircuitBoard,Gear}

    private void Start() {
        //empty boxes at the start
        EmptyBoxes();
    }

    public void TriggerDamagePart(){
        BoxColor box = Utilities.GetRandomEnum<BoxColor>();
        ItemPart itemPart = Utilities.GetRandomEnum<ItemPart>();

        StartCoroutine(DamagePart(itemPart, box));        
    }

    private IEnumerator DamagePart(ItemPart itemPart, BoxColor box){       
        
        Item item;

        switch(itemPart)
        {
            case ItemPart.Battery:
                item = _itemBattery;
                break;
            case ItemPart.CircuitBoard:
                item = _itemCircuitBoard;
                break;
            case ItemPart.Gear:
                item = _itemGear;
                break;
            default:
                item = _itemBattery;
                break;
        }

        BoxContainer boxContainer;

        switch(box)
        {
            case BoxColor.Red:
                boxContainer = redBox;
                break;
            case BoxColor.Blue:
                boxContainer = blueBox;
                break;
            case BoxColor.Green:
                boxContainer = redBox;
                break;
            default:
                boxContainer = redBox;
                break;
        }
        
        //triggler bubble (if not running)
        bool bubbleNotActive = false;

        while (!bubbleNotActive){ // keep trying to trigger next part once the spot becomes available.
            bubbleNotActive = _bubble.TriggerBubble(boxContainer.boxColor, item.itemIcon);
            yield return null;
        }

        boxContainer.AddRequiredItemtoBox(item);
    }

    private void EmptyBoxes(){
        blueBox.EmptyBox();
        redBox.EmptyBox();
        greenBox.EmptyBox();
    }
}
