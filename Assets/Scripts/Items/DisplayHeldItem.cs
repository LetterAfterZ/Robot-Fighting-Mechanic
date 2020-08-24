using UnityEngine;
using UnityEngine.UI;

public class DisplayHeldItem : MonoBehaviour
{
    [SerializeField] Image _image = null;
    [SerializeField] PlayerHeldItem _playerHeldItem = null;

    public void UpdateItemDisplay(){
        bool getItem = _playerHeldItem.ReturnCurrentItem(out Item item);

        if(getItem){
            _image.enabled = true;
            _image.sprite = item.itemIcon;
        }else{
            _image.enabled = false;
        }
    }
}
