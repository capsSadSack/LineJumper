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
    };

    private Dictionary<Achievement, string> descriptions = new Dictionary<Achievement, string>()
    {
        { Achievement.Runaway, "Escape from the playing field" },
        { Achievement.DoubleKill, "Destroy two enemies at the same player's jump" },
    };

}
