using System.Collections.Generic;

public interface IAchievementsAccess
{
    Dictionary<Achievement, bool> GetAchievementsStates();

    void SetAchievementState(Achievement achievement, bool state);
}

