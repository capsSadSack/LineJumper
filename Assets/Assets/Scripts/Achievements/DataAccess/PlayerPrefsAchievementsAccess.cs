using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsAchievementsAccess : IAchievementsAccess
{
    private AchievementsData achievementsData = new AchievementsData();


    public Dictionary<Achievement, bool> GetAchievementsStates()
    {
        Dictionary<Achievement, bool> output = new Dictionary<Achievement, bool>();

        IEnumerable<Achievement> allAchievements = EnumsProcessor.GetAllValues(Achievement.Runaway);
        foreach (var achievement in allAchievements)
        {
            var key = GetStringKey(achievement);
            bool isUnlocked = PlayerPrefs.GetInt(key, 0) == 1;
            output.Add(achievement, isUnlocked);
        }

        return output;
    }

    public void SetAchievementState(Achievement achievement, bool state)
    {
        var key = GetStringKey(achievement);

        PlayerPrefs.SetInt(key, state ? 1 : 0);
        PlayerPrefs.Save();
    }

    private string GetStringKey(Achievement achievement)
    {
        string achievementName = achievementsData.GetName(achievement);
        return $"ach_{ achievementName }";
    }
}
