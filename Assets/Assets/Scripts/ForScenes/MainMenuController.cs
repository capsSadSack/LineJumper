using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("DifficultyScene");
    }

    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void LoadRecordsScene()
    {
        SceneManager.LoadScene("RecordsScene");
    }

    public void LoadAchievementsScene()
    {
        SceneManager.LoadScene("AchievementsScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
