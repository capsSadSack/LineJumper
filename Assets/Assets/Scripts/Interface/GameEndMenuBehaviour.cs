using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndMenuBehaviour : MonoBehaviour
{
    public static bool GameEnded = false;

    public GameEndEvent OnGameEnd;
    public AchievementUnlockedEvent OnAchievementUnlocked;

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

        isRecord = recordsAccess.IsRecord(recordInfo);
        endGameMessageText.text = (isRecord) ? "Great Result!" : "Game over";

        enterNameText.gameObject.SetActive(isRecord);
        nameTextBox.gameObject.SetActive(isRecord);
    }


    public void Restart()
    {
        SaveRecord();
        NotifyGameEnd();

        Time.timeScale = 1f;
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void ToMainMenu()
    {
        SaveRecord();
        NotifyGameEnd();

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    private void NotifyGameEnd()
    {
        GameEndArgs args = new GameEndArgs()
        {
            Difficulty = difficultyAccess.GetDifficulty(),
            Player = nameTextBox.text,
            Score = scoreController.Score
        };

        CheckGameResultForAchievements(args);
        OnGameEnd.Invoke(args);
    }

    private void SaveRecord()
    {
        var name = nameTextBox.text.Replace(";","");
        recordInfo.PlayerName = name;

        recordsAccess.InsertRecord(recordInfo);
    }

    private void CheckGameResultForAchievements(GameEndArgs args)
    {
        if (args.Score >= 250)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_250
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Score >= 100)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_100
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Score >= 50)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_50
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Score >= 25)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_25
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Score >= 10)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_10
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }
    }
}
