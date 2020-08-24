using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoxContainer : ScriptableObject
{
    public int boxID;
    public string boxName;

    public Color boxColor;

    //parts broken in the fight
    public List<Item> boxRequiredParts;

    //parts added by the player
    public List<Item> boxAddedParts;

    public void AddRequiredItemtoBox(Item item){
        boxRequiredParts.Add(item);
    }

    public void PlayerAddItemToBox(Item item){
        boxAddedParts.Add(item);
    }

    public void EmptyBox(){
        boxRequiredParts.Clear();
        boxAddedParts.Clear();
    }
}
