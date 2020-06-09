using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMessageShower : MonoBehaviour
{
    public GameObject achievementCanvas;

    public Sprite runawayIcon;


    private IAchievementsAccess achievementsAccess 
        = new PlayerPrefsAchievementsAccess();
    private AchievementsData achievementsData =
        new AchievementsData();

    private const int messageShowDuration_Sec = 5;

    private List<ShowingMessage> messages = new List<ShowingMessage>();


    public void ShowAchievementMessage(AchievementUnlockedArgs args)
    {
        this.achievementCanvas.SetActive(true);
        this.gameObject.SetActive(true);

        // TODO: [CG, 2020.05.31] Заглушка на показ достижений всегда
        if (!achievementsAccess.GetAchievementsStates()[args.Achievement])
        {
            GameObject message = CreateAchievementMessage(args.Achievement);

            message.transform.SetParent(this.transform);

            message.transform.localScale = new Vector3(1, 1, 1);
            message.transform.rotation = new Quaternion(0, 0, 0, 0);

            RectTransform rt = message.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0);
            rt.anchorMax = new Vector2(0.5f, 0);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition3D = GetPosition(messages.Count - 1);

            ShowingMessage sm = new ShowingMessage()
            {
                Message = message,
                IsMessageShowing = true,
                MessageAppearTime_Utc = DateTime.UtcNow
            };

            messages.Add(sm);
        }
    }

    private GameObject CreateAchievementMessage(Achievement achievement)
    {
        // NOTE: Создание экземпляра prefab'а из кода.
        var source = Resources.Load("Prefabs/AchievementUnlocked");
        GameObject objSource = (GameObject)Instantiate(source);

        foreach (var image in objSource.GetComponentsInChildren<Image>())
        {
            if (image.name == "UnlockedMask")
            {
                image.gameObject.SetActive(false);
            }
        }

        UpdateAchievementMessage(objSource, achievement);

        return objSource;
    }

    private Sprite GetSprite(Achievement achievement)
    {
        switch (achievement)
        {
            case Achievement.Runaway:
                return runawayIcon;
            default:
                throw new ArgumentException("AchievementMessageShower.GetSprite: " +
                    "нет спрайта для выбранного Achievement.");
        }
    }

    private void UpdateAchievementMessage(GameObject message, Achievement achievement)
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

    private Vector3 GetPosition(int orderNumber)
    {
        return new Vector3(0, 130 + (200 + 30) * orderNumber, 0);
    }

    private void Update()
    {
        if (messages.Count > 0)
        {
            this.gameObject.SetActive(true);

            DeleteOldMessages();
            UpdatePositions();

            if (messages.Count == 0)
            {
                HidePanel();
            }
        }
    }

    private void HidePanel()
    {
        this.achievementCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void DeleteOldMessages()
    {
        DateTime now = DateTime.UtcNow;

        for (int i = 0; i < messages.Count; i++)
        {
            if ((now - messages[i].MessageAppearTime_Utc).TotalSeconds > messageShowDuration_Sec)
            {
                GameObject.Destroy(messages[i].Message);
                messages.RemoveAt(i);
                i--;
            }
        }
    }

    private void UpdatePositions()
    {
        for (int i = 0; i < messages.Count; i++)
        {
            var rt = messages[i].Message.GetComponent<RectTransform>();
            rt.anchoredPosition3D = GetPosition(i);
        }
    }
}
