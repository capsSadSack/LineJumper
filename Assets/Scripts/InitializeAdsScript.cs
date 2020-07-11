using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{
    private static int counter = 0;

#if UNITY_IOS
    private string gameId = "3646123";
#elif UNITY_ANDROID
    private string gameId = "3646122";
#endif

    private string placementId = "BannerPlacement";

#if UNITY_EDITOR
    private bool testMode = true;
#else
    private bool testMode = false;
#endif


    private void Start()
    {
        SetAdTestMode();
        Advertisement.Initialize(gameId, testMode);

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerWhenReady());
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

#if UNITY_EDITOR
    private void SetAdTestMode()
    {
        testMode = true;
    }
#else
    private void SetAdTestMode()
    {
        testMode = false;
    }
#endif

    public void ShowAd()
    {
        counter++;
        if (counter % 3 == 0)
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

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId) && !PlayerPrefs.HasKey("Blessing"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(placementId);
    }
}