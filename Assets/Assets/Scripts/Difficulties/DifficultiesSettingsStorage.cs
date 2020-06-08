using Assets.Assets.Scripts.Difficulties;
using System.Collections.Generic;

public static class DifficultiesSettingsStorage
{
    public static Dictionary<Difficulty, DifficultySettings> Settings =
        new Dictionary<Difficulty, DifficultySettings>()
        {
                { Difficulty.Easy,   easySettings   },
                { Difficulty.Medium, mediumSettings },
                { Difficulty.Hard,   hardSettings   }
        };

    private static DifficultySettings easySettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 1,
            EnemiesSpawnPeriod_Sec = 5,
            GameActionPeriod_Sec = 3
        };

    private static DifficultySettings mediumSettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 3,
            EnemiesSpawnPeriod_Sec = 4,
            GameActionPeriod_Sec = 2
        };

    private static DifficultySettings hardSettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 5,
            EnemiesSpawnPeriod_Sec = 3,
            GameActionPeriod_Sec = 1.5f
        };
}
