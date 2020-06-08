using System;
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
    public InputField nameTextBox;

    private bool isRecord;
    private RecordInfo recordInfo;
    private PlayerPrefsRecordsAccess recordsAccess = new PlayerPrefsRecordsAccess();
    private PlayerPrefsDifficultyAccess difficultyAccess = new PlayerPrefsDifficultyAccess();

    public void ShowGameEndMenu()
    {
        GameEnded = true;
        gameInterface.SetActive(false);
        gameEndMenuUI.SetActive(true);
        Time.timeScale = 0f;

        int score = scoreController.Score;
        scoreValue.text = $"{ score }";

        recordInfo = new RecordInfo()
        {
            Date = DateTime.UtcNow.Date,
            Difficulty = difficultyAccess.GetDifficulty(),
            PlayerName = "name",
            Score = score
        };

        // TODO: Заглушка. Учитывать рекорды
        isRecord = recordsAccess.IsRecord(recordInfo);

        endGameMessageText.text = (isRecord) ? "Great Result!" : "Game over";

        enterNameText.gameObject.SetActive(isRecord);
        nameTextBox.gameObject.SetActive(isRecord);
    }


    public void Restart()
    {
        SaveRecord();

        Time.timeScale = 1f;
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void ToMainMenu()
    {
        SaveRecord();

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    private void SaveRecord()
    {
        var name = nameTextBox.text.Replace(";","");
        recordInfo.PlayerName = name;

        recordsAccess.InsertRecord(recordInfo);
    }
}
