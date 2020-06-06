using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadHowToPlayScene()
    {
        throw new NotImplementedException();
    }

    public void LoadRecordsScene()
    {
        throw new NotImplementedException();
    }

    public void LoadAchievementsScene()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
