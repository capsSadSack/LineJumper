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

    public GameObject highScores;

    private bool isRecord;
    private RecordInfo recordInfo;
    private readonly PlayerPrefsRecordsAccess recordsAccess = new PlayerPrefsRecordsAccess();
    private readonly PlayerPrefsDifficultyAccess difficultyAccess = new PlayerPrefsDifficultyAccess();


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

        GameEndArgs args = new GameEndArgs()
        {
            Record = recordInfo
        };

        CheckGameResultForAchievements(args);
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
            Record = recordInfo
        };

        OnGameEnd.Invoke(args);
    }

    private void SaveRecord()
    {
        var name = nameTextBox.text.Replace(";","");
        recordInfo.PlayerName = name;

        recordsAccess.InsertRecord(recordInfo);
        highScores.GetComponent<HighScores>().AddNewHighScore(recordInfo);
    }

    private void CheckGameResultForAchievements(GameEndArgs args)
    {
        if (args.Record.Score >= 250)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_250
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Record.Score >= 100)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_100
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Record.Score >= 50)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_50
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Record.Score >= 25)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_25
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }

        if (args.Record.Score >= 10)
        {
            AchievementUnlockedArgs achArgs = new AchievementUnlockedArgs()
            {
                Achievement = Achievement.SingleGameCollected_10
            };

            OnAchievementUnlocked.Invoke(achArgs);
        }
    }
}
