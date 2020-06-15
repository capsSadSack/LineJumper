using UnityEngine;

public class AchievementDataSaver : MonoBehaviour
{
    private IAchievementsAccess achievementsAccess 
        = new PlayerPrefsAchievementsAccess();


    public void UnlockAchievement(AchievementUnlockedArgs args)
    {
        // TODO: [CG, 2020.06.08] Заглушка - получаемые достижения отображаются всегда
        if (!achievementsAccess.GetAchievementsStates()[args.Achievement])
        {
            achievementsAccess.SetAchievementState(args.Achievement, true);
        }
    }

    public void ResetAchievements()
    {
        var allAchievements = EnumsProcessor.GetAllValues(Achievement.Runaway);

        foreach (var achievement in allAchievements)
        {
            achievementsAccess.SetAchievementState(achievement, false);
        }
    }
}
