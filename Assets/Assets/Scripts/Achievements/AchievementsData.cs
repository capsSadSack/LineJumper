using System.Collections.Generic;

public class AchievementsData
{
    public string GetName(Achievement achievement)
    {
        return names[achievement];
    }

    public string GetDescription(Achievement achievement)
    {
        return descriptions[achievement];
    }

    private Dictionary<Achievement, string> names = new Dictionary<Achievement, string>()
    {
        { Achievement.Runaway, "Runaway" },
        { Achievement.DoubleKill, "Double Kill" },
        { Achievement.StayStill, "Stay Still" },
        { Achievement.WorkAndRest, "Work and Rest" },

        { Achievement.SingleGameCollected_10, "Scrapper" },
        { Achievement.SingleGameCollected_25, "Brawler" },
        { Achievement.SingleGameCollected_50, "Warrior" },
        { Achievement.SingleGameCollected_100, "Veteran" },
        { Achievement.SingleGameCollected_250, "Destroyer" },
    };

    private Dictionary<Achievement, string> descriptions = new Dictionary<Achievement, string>()
    {
        { Achievement.Runaway, "Escape from the playing field" },
        { Achievement.DoubleKill, "Destroy two enemies at the same player's jump" },
        { Achievement.StayStill, "Do not move for 30 seconds" },
        { Achievement.WorkAndRest, "Destroy an enemy being on a line" },

        { Achievement.SingleGameCollected_10, "Destroy 10 enemies in a single game" },
        { Achievement.SingleGameCollected_25, "Destroy 25 enemies in a single game" },
        { Achievement.SingleGameCollected_50, "Destroy 50 enemies in a single game" },
        { Achievement.SingleGameCollected_100, "Destroy 100 enemies in a single game" },
        { Achievement.SingleGameCollected_250, "Destroy 250 enemies in a single game" },
    };

}
