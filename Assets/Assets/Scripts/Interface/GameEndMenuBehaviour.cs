using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndMenuBehaviour : MonoBehaviour
{
    public static bool GameEnded = false;
    public GameObject gameEndMenuUI;
    public GameObject gameInterface;

    public Text scoreValue;
    public ScoreController scoreController;

    public Text endGameMessageText;
    public Text enterNameText;
    public GameObject nameTextBox;

    private bool isRecord;

    public void SowGameEndMenu()
    {
        GameEnded = true;
        gameInterface.SetActive(false);
        gameEndMenuUI.SetActive(true);
        Time.timeScale = 0f;

        int score = scoreController.Score;
        scoreValue.text = $"{ score }";

        // TODO: Заглушка. Учитывать рекорды
        isRecord = score > 10;

        endGameMessageText.text = (isRecord) ? "Great Result!" : "Game over";

        enterNameText.gameObject.SetActive(isRecord);
        nameTextBox.gameObject.SetActive(isRecord);
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
