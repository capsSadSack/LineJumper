﻿using System;
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
        throw new NotImplementedException();
    }

    public void LoadRecordsScene()
    {
        SceneManager.LoadScene("RecordsScene");
    }

    public void LoadAchievementsScene()
    {
        SceneManager.LoadScene("AchievementsSceneController");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
