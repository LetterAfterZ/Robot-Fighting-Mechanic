using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Transform _bubble = null;
    [SerializeField] private SpriteRenderer _bubbleSR = null;
    [SerializeField] private SpriteRenderer _bubbleIconSR = null;

    [SerializeField] private float _bubbleGrowSpeed = 50f;

    private bool _bubbleActive = false;

    private void Start() {
        //start bubbles hidden
        _bubbleSR.enabled = false;
        _bubbleIconSR.enabled = false;
    }

    public bool TriggerBubble(Color color, Sprite item){
        if (!_bubbleActive){
            _bubbleActive = true;
            StartCoroutine(ShowBubble(color, item));
            return true;
        }else{
            return false; // don't show the bubble if one is already being shown
        }
    }

    private IEnumerator ShowBubble(Color color, Sprite item){
        _bubbleSR.enabled = false;
        _bubbleIconSR.enabled = false;

        _bubbleSR.color = color;
        _bubble.localScale = new Vector3(0,0,0);
        _bubbleIconSR.sprite = item;

        _bubbleSR.enabled = true;
        _bubbleIconSR.enabled = true;
    
        //animate in bubble
        while(_bubble.localScale.x < 1){ //bubble isn't full size
            _bubble.localScale += new Vector3(0.1f,0.1f,0.1f) * Time.deltaTime * _bubbleGrowSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while(_bubble.localScale.x > 0){ //bubble isn't full size
            _bubble.localScale -= new Vector3(0.1f,0.1f,0.1f) * Time.deltaTime * _bubbleGrowSpeed;
            yield return null;
        }
        _bubbleSR.enabled = false;
        _bubbleIconSR.enabled = false;

        //reanble the script
        _bubbleActive = false;
    }
}
