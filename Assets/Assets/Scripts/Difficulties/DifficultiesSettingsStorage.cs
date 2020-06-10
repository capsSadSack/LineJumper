using Assets.Assets.Scripts.Difficulties;
using System.Collections.Generic;

public static class DifficultiesSettingsStorage
{
    private readonly static DifficultySettings easySettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 3,
            EnemiesSpawnPeriod_Sec = 3,
            GameActionPeriod_Sec = 2.0f,
            MinVelocity = 3,
            MaxVelocity = 5
        };

    private readonly static DifficultySettings mediumSettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 4,
            EnemiesSpawnPeriod_Sec = 2.5f,
            GameActionPeriod_Sec = 1.75f,
            MinVelocity = 4,
            MaxVelocity = 6
        };

    private readonly static DifficultySettings hardSettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 5,
            EnemiesSpawnPeriod_Sec = 2,
            GameActionPeriod_Sec = 1.5f,
            MinVelocity = 4,
            MaxVelocity = 8
        };

    public static Dictionary<Difficulty, DifficultySettings> Settings { get; private set; } =
    new Dictionary<Difficulty, DifficultySettings>()
    {
                { Difficulty.Easy,   easySettings   },
                { Difficulty.Medium, mediumSettings },
                { Difficulty.Hard,   hardSettings   }
    };
}
