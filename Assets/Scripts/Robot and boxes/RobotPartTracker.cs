using UnityEngine;

public class RobotPartTracker : MonoBehaviour
{
    public BoxContainer blueBox;
    public BoxContainer redBox;
    public BoxContainer greenBox;    

    [SerializeField] private Item _itemBattery = null;
    [SerializeField] private Item _itemCircuitBoard = null;
    [SerializeField] private Item _itemGear= null;

    [SerializeField] private Bubble _bubble= null;

    private enum BoxColor {Red,Blue,Green}
    private enum ItemPart {Battery,CircuitBoard,Gear}

    private void Awake() {
        //empty boxes at the start
        EmptyBoxes();
    }

    public void TriggerDamagePart(){
        BoxColor box = Utilities.GetRandomEnum<BoxColor>();
        ItemPart itemPart = Utilities.GetRandomEnum<ItemPart>();

        DamagePart(itemPart, box);    
    }

    private void DamagePart(ItemPart itemPart, BoxColor box){       
        
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
        
        bool success = _bubble.TriggerBubble(boxContainer.boxColor, item.itemIcon);

        if(success){             
            boxContainer.AddRequiredItemtoBox(item);
        }
    }

    private void EmptyBoxes(){
        blueBox.EmptyBox();
        redBox.EmptyBox();
        greenBox.EmptyBox();
    }
}
