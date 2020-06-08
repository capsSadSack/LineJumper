using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultiesSceneController : MonoBehaviour
{
    private PlayerPrefsDifficultyAccess difficultyAccess;

    private void Start()
    {
        difficultyAccess = new PlayerPrefsDifficultyAccess();
    }

    public void SetEasyDifficulty() => difficultyAccess.SetDifficulty(Difficulty.Easy);

    public void SetMediumDifficulty() => difficultyAccess.SetDifficulty(Difficulty.Medium);

    public void SetHardDifficulty() => difficultyAccess.SetDifficulty(Difficulty.Hard);


    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
