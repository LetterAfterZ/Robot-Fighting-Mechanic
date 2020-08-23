using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHeldItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] PlayerHeldItem playerHeldItem = null;

    public void UpdateItemDisplay(){
        bool getItem = playerHeldItem.ReturnCurrentItem(out Item item);

        if(getItem){
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = item.itemIcon;
        }else{
            spriteRenderer.enabled = false;
        }
    }
}
