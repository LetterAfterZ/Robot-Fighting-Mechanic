using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;
    [SerializeField] private Animator _playerRobotAnimator = null;
    [SerializeField] private RobotHealth _robotHealth = null;
    [SerializeField] private PlayerMovement _playerMovement = null;

    private void Start() {
        //reset player held item on game start
        _playerHeldItem?.TrashItem();

        AudioManager.instance.Play("crowd");
    }

    void Update()
    {
        if (Input.GetKeyUp("escape")) // quit to main menu if pressing escape			
			SceneManager.LoadScene(0);
    }

    public void GameOverLose(){
        //todo play player robot death, then load game over screen
        SceneManager.LoadScene("GameOverLose");
    }

    public void GameOverWin(){
        //play laser scene, then load game over win screen (coroutine)
        StartCoroutine(endScene());        
    }

    private IEnumerator endScene(){
        _playerMovement.SetPlayerMovement(false);
        _robotHealth.TakeDamage(-1000);
        _playerRobotAnimator.Play("Robot_Attack_2");
        
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOverWin");
    }
}
