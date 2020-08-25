using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float _fightTime = 40f;
    [SerializeField] float _repairTime = 20f;

    [SerializeField] public float currentCountDown;
    [SerializeField] public bool isFight = false;

    [SerializeField] RectTransform _clockhand = null;
    [SerializeField] Image _clockface = null;
    [SerializeField] Sprite _clockBattle = null;
    [SerializeField] Sprite _clockRepair = null;


    [SerializeField] private GameEvent _newFightRound = null;
    //[SerializeField] private GameEvent _newRepairRound = null;

    [SerializeField] private EvalRepairs _evalRepairs = null;

    private void Start() {
        StartNextMode();
    }

    void StartNextMode(){
        if (!isFight){
            //trigger eval
            _evalRepairs.EvaluateRepairs();
            isFight = true;
            _clockface.sprite = _clockBattle;    
            currentCountDown = _fightTime;
            // fire off new round event
            _newFightRound.Raise();
            
        }else{
            isFight = false;     
            _clockface.sprite = _clockRepair;    
            currentCountDown = _repairTime;         
        }
        StartCoroutine(CountdownTime());
    }

    IEnumerator CountdownTime(){
        float totalCountdownTime = currentCountDown;
        
        while(currentCountDown > 0){
            currentCountDown -= Time.deltaTime;

            //rotate clock hand
            _clockhand.eulerAngles = new Vector3(0,0, (currentCountDown / totalCountdownTime) * 360);
            yield return null;
        }

        StartNextMode();
    }
}
