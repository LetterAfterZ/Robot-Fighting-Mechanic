using System.Collections;
using UnityEngine;

public class TimingMinigame : Minigame
{
    [SerializeField] private RectTransform _background = null;
    [SerializeField] private RectTransform _target = null;
    [SerializeField] private RectTransform _marker = null;
    [SerializeField] private float _markerMoveSpeed = 500f;
    [SerializeField] private float _edgeOffset = 5f;    

    private Vector2 _leftLimit;
    private Vector2 _rightLimit;
    private Vector2 _moveTarget;
    private bool _movingLeft = true;


    protected override void Start() {
        base.Start();
        
        //get length of the bar for left right limits
        _leftLimit = new Vector2(_background.offsetMin.x + _edgeOffset, _marker.anchoredPosition.y);
        _rightLimit = new Vector2(_background.offsetMax.x - _edgeOffset, _marker.anchoredPosition.y);

        //set initial move target to the left
        _moveTarget = _leftLimit;

        // start coroutine to make ball bounce between em
        StartCoroutine(StartMinigame());
    }

    protected void Update() {
        if(Input.GetKeyDown("space") && !_failTriggered ) { //check for player keypresses
            if (CheckOverlap(_marker, _target)){
                TriggerSuccess();
            }else{
                TriggerFailure();
            }
        }
    }

    //convert two rect transforms to rects and check if they overlap
    private bool CheckOverlap(RectTransform rectTrans1, RectTransform rectTrans2){
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }


    private IEnumerator StartMinigame(){    
        while (true) //loop endlessly
        {
            if(_failTriggered) { // check for failure
                // Reset shake counter and trigger shake
                _shakeCounter = 0;
                StartCoroutine(FailShake());
                yield return new WaitForSeconds(_timeBetweenFails);
                _failTriggered = false;
            }
            MoveBackAndForth();
            yield return null;
        }
    }

    private void MoveBackAndForth() {
        //move marker
        _marker.anchoredPosition = Vector2.MoveTowards(_marker.anchoredPosition,_moveTarget,_markerMoveSpeed * Time.deltaTime);

        //switch direction at the end
        if(_marker.anchoredPosition.x == _moveTarget.x) {
            if(_movingLeft) {
                _moveTarget = _rightLimit;
                _movingLeft = false;
            } else {
                _moveTarget = _leftLimit;
                _movingLeft = true;
            }
        }
    }
}
