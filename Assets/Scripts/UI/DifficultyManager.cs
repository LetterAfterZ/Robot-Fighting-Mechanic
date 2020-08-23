using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] public static int GameDifficulty = 0;

    public void IncreaseDifficulty(){
        GameDifficulty++;
    }
}
