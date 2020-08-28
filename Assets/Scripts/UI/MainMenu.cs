using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start() {
        //play theme 
        AudioManager.instance.Play("theme");
    }

    public void PlayGame (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame (){
        Application.Quit();
    }
}
