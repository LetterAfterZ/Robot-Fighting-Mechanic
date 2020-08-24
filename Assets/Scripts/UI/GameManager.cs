using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;

    private void Start() {
        //reset player held item on game start
        _playerHeldItem?.TrashItem();
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
        SceneManager.LoadScene("GameOverWin");
    }
}
