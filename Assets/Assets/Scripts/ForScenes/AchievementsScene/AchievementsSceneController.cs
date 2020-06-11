using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementsSceneController : MonoBehaviour
{
    public Sprite runawayIcon;
    public Sprite doubleKillIcon;
    public Sprite stayStillIcon;
    public Sprite workAndRestIcon;

    public Sprite destroyInSingleGame10Icon;
    public Sprite destroyInSingleGame25Icon;
    public Sprite destroyInSingleGame50Icon;
    public Sprite destroyInSingleGame100Icon;
    public Sprite destroyInSingleGame250Icon;


    public GameObject achievementsPanel;

    private IAchievementsAccess achievementsAccess = new PlayerPrefsAchievementsAccess();
    private AchievementsData achievementsData = new AchievementsData();

    private Text achievementsUnlockedText;
    


    private void Start()
    {
        achievementsUnlockedText = GameObject.Find("UnlockedAchievementsText").GetComponent<Text>();

        ReadAchievements();
    }

    private void ReadAchievements()
    {
        Dictionary<Achievement, bool> achievementsStates = achievementsAccess.GetAchievementsStates();

        UpdateAchievementsText(achievementsStates);
        ShowAchievements(achievementsStates);
    }

    private void UpdateAchievementsText(Dictionary<Achievement, bool> achievementsStates)
    {
        var total = achievementsStates.Count;
        var unlocked = achievementsStates.Where(x => x.Value == true).Count();

        achievementsUnlockedText.text = $"{ unlocked } / {total}";
    }

    private void ShowAchievements(Dictionary<Achievement, bool> achievementsStates)
    {
        Vector2 offset = new Vector2(0, -400);
        float dy = -250;
        Vector2 shift = new Vector2(0, dy);

        for (int i = 0; i < achievementsStates.Count; i++)
        {
            var item = achievementsStates.ElementAt(i);
            GameObject achievementObj = CreateAchievement(item.Key, item.Value);

            achievementObj.transform.SetParent(achievementsPanel.transform);
            achievementObj.transform.localScale = new Vector3(1, 1, 1);

            var rt = achievementObj.gameObject.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
            rt.anchoredPosition = offset + (i - 1) * shift;
        }
    }


    private GameObject CreateAchievement(Achievement achievement, bool isUnlocked)
    {
        // NOTE: Создание экземпляра prefab'а из кода.
        var source = Resources.Load("Prefabs/AchievementUnlocked");
        GameObject objSource = (GameObject)Instantiate(source);
        objSource.name = EnumsProcessor.GetDescription(achievement);

        // NOTE: Настройка полупрозрачной маски закрытого достижения (есть/нет)
        objSource.transform.GetChild(4).gameObject.SetActive(!isUnlocked);

        CustomizeAchievementMessage(objSource, achievement);

        return objSource;
    }

    private void CustomizeAchievementMessage(GameObject message, Achievement achievement)
    {
        string description = achievementsData.GetDescription(achievement);
        string name = achievementsData.GetName(achievement);
        Sprite sprite = GetSprite(achievement);

        var texts = message.GetComponentsInChildren<Text>();

        foreach (var text in texts)
        {
            if (text.name == "Name")
            {
                text.text = name;
            }

            if (text.name == "Text")
            {
                text.text = description;
            }
        }

        var images = message.GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            if (image.name == "Icon")
            {
                image.sprite = sprite;
            }
        }
    }

    private Sprite GetSprite(Achievement achievement)
    {
        switch (achievement)
        {
            case Achievement.Runaway:
                return runawayIcon;
            case Achievement.DoubleKill:
                return doubleKillIcon;
            case Achievement.StayStill:
                return stayStillIcon;
            case Achievement.WorkAndRest:
                return workAndRestIcon;
            case Achievement.SingleGameCollected_10:
                return destroyInSingleGame10Icon;
            case Achievement.SingleGameCollected_25:
                return destroyInSingleGame25Icon;
            case Achievement.SingleGameCollected_50:
                return destroyInSingleGame50Icon;
            case Achievement.SingleGameCollected_100:
                return destroyInSingleGame100Icon;
            case Achievement.SingleGameCollected_250:
                return destroyInSingleGame250Icon;
            default:
                throw new ArgumentException("AchievementMessageShower.GetSprite: " +
                    "нет спрайта для выбранного Achievement.");
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
