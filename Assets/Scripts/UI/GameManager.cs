using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private PlayerHeldItem _playerHeldItem = null;

    private void Start() {
        //reset player held item on game start
        _playerHeldItem.TrashItem();
    }
    void Update()
    {
        if (Input.GetKeyUp("escape")) // quit to main menu if pressing escape			
			SceneManager.LoadScene(0);		
    }
}
