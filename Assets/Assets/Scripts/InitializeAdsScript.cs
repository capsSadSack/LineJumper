using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{
    private string gameId = "3646122";
    private bool testMode = true;

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void LockAd(GameEndArgs args)
    {
        if (!PlayerPrefs.HasKey("Blessing"))
        {
            if (args.Difficulty == Difficulty.Hard &&
                args.Score == 3 &&
                args.Player == "Глеб, ты супер!")
            {
                PlayerPrefs.SetInt("Blessing", 0);
                PlayerPrefs.Save();
            }
        }
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}