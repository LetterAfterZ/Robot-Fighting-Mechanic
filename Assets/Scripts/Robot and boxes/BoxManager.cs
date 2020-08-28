using System.Collections;
using UnityEngine;

public class BoxManager : MonoBehaviour
{

    [SerializeField] GameObject[] boxes = null;
    
    public void TriggerEnableBoxes(){
        StartCoroutine(EnableBoxes());  
    }

    public void TriggerDisableBoxes(){
        StartCoroutine(DisableBoxes());  
    }
    
    IEnumerator EnableBoxes(){
        foreach (GameObject box in boxes){
            box.SetActive(true);
        }
        yield break;  
    }

    IEnumerator DisableBoxes(){
        foreach (GameObject box in boxes){
            box.SetActive(false);
        }
        yield break;  
    }
}
