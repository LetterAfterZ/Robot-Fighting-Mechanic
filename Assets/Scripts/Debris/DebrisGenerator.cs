using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisGenerator : MonoBehaviour
{
    [SerializeField] Debris _debris = null;
    [SerializeField] Transform _playerRobot = null;
    [SerializeField] Animator _enemyRobot = null;
    [SerializeField] Effect _blastEffect = null;
    [SerializeField] GameTimer _gameTimer = null;
    //[SerializeField] RobotHealth _robotHealth = null;
    [SerializeField] SoundRandomiser _impactPunch = null;
    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] GameEvent _playerRobotPartDamaged = null;

    [SerializeField] float _initialSpawnDebris = 3f;
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
            spawnTimes.Add(Random.Range(0, _gameTimer.currentCountDown - 2f));
        }
            
        //SPAWN DEBRIS AT SPAWN TIMES
        while (spawnTimes.Count > 0){                    
            for(int i=spawnTimes.Count -1; i > 0; i--){
                if (spawnTimes[i] >= _gameTimer.currentCountDown - 2f){    
                    StartCoroutine(SpawnDebris()); 
                    spawnTimes.RemoveAt(i);
                }
            }
            yield return null;
        }
    }


    private IEnumerator SpawnDebris(){    
        // enemy robot attacks
        _enemyRobot.GetComponent<Animator>().SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.2f);
        _enemyRobot.GetComponent<Animator>().SetBool("IsAttacking", false);

        //trigger part damage
        _playerRobotPartDamaged.Raise();

        // get player position for debris spawn
        Vector2 originPoint = _playerRobot.position;
        
        //play impact sound
        _audioSource.PlayOneShot(_impactPunch.ReturnRandomSound());

        // play explosion effect on the player
        Instantiate(_blastEffect, originPoint, Quaternion.identity);        

        // spawn Debris (Debris can self randomise)
        Instantiate(_debris, originPoint, Quaternion.identity);
        
    }
}
