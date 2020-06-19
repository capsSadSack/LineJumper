﻿using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{
    private static int counter = 0;

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
            if (args.Record.Difficulty == Difficulty.Easy &&
                args.Record.Score == 3 &&
                args.Record.PlayerName == "xxXg53e1Hsrhg")
            {
                PlayerPrefs.SetInt("Blessing", 0);
                PlayerPrefs.Save();
            }
        }
    }

    public void ShowAd()
    {
        counter++;
        if (counter % 5 == 0)
        {
            if (!PlayerPrefs.HasKey("Blessing"))
            {
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                }
            }

            counter = 0;
        }
    }
}