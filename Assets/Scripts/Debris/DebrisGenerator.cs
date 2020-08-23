using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisGenerator : MonoBehaviour
{
    [SerializeField] Debris _debris = null;
    [SerializeField] Transform[] _robots = null;
    [SerializeField] Effect _blastEffect = null;
    [SerializeField] GameTimer _gameTimer = null;

    [SerializeField] float _initialSpawnDebris = 3;
    [SerializeField] float _extraDebrisPerRound = 1.2f;

    public void StartSpawner(){ //triggered by event listener        
        StartCoroutine(SpawnRandomDebris());
    }

    private IEnumerator SpawnRandomDebris()
    {    
        // SET UP DEBRIS SPAWN TIMES BASED ON DIFF
        float spawnCount = _initialSpawnDebris + (_extraDebrisPerRound * DifficultyManager.GameDifficulty);
        int spawnCountInt = System.Convert.ToInt32(Mathf.Floor(spawnCount));

        //float[] spawnTimes = new float[spawnCountInt];
        List<float> spawnTimes = new List<float>();

        for (int i=0; i < spawnCountInt; i++){
            spawnTimes.Add(Random.Range(0, _gameTimer.currentCountDown));
        }
            
        //SPAWN DEBRIS AT SPAWN TIMES
        while (spawnTimes.Count > 0){                    
            for(int i=spawnTimes.Count -1; i > 0; i--){
                if (spawnTimes[i] >= _gameTimer.currentCountDown){    
                    StartCoroutine(SpawnDebris()); 
                    spawnTimes.RemoveAt(i);
                }
            }
            yield return null;
        }
    }


    private IEnumerator SpawnDebris(){
        //pick random robot
        int robotIndex = Random.Range(0, _robots.Length);
        Vector2 originPoint = _robots[robotIndex].position;

        // make OTHER robot punch
        // TODO move this to a script and make it move and attack, 
        // or vary attack and return diff animation time for waiting
        _robots[robotIndex == 0 ? 1:0].GetComponent<Animator>().SetBool("IsAttacking", true);
        yield return new WaitForSeconds(1f);
        _robots[robotIndex == 0 ? 1:0].GetComponent<Animator>().SetBool("IsAttacking", false);

        // play explosion effect on one of the two robots randomly
        Instantiate(_blastEffect, originPoint, Quaternion.identity);

        // spawn Debris (Debris can self randomise)
        Instantiate(_debris, originPoint, Quaternion.identity);


    }
}
