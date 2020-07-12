using Assets.Assets.Scripts.Difficulties;
using System.Collections.Generic;

public static class DifficultiesSettingsStorage
{
    private readonly static DifficultySettings easySettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 3,
            InitialEnemiesSpawnPeriod_Sec = 4.5f,
            MinimumEnemiesSpawnPeriod_Sec = 3.0f,
            GameActionPeriod_Sec = 3.0f,
            MinVelocity = 1.5f,
            MaxVelocity = 3
        };

    private readonly static DifficultySettings mediumSettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 4,
            InitialEnemiesSpawnPeriod_Sec = 4.0f,
            MinimumEnemiesSpawnPeriod_Sec = 2.5f,
            GameActionPeriod_Sec = 2.5f,
            MinVelocity = 2.5f,
            MaxVelocity = 3.75f
        };

    private readonly static DifficultySettings hardSettings
        = new DifficultySettings()
        {
            InitialEnemiesCount = 5,
            InitialEnemiesSpawnPeriod_Sec = 3.5f,
            MinimumEnemiesSpawnPeriod_Sec = 2.25f,
            GameActionPeriod_Sec = 2.25f,
            MinVelocity = 3,
            MaxVelocity = 4.25f
        };

    public static Dictionary<Difficulty, DifficultySettings> Settings { get; private set; } =
    new Dictionary<Difficulty, DifficultySettings>()
    {
        { Difficulty.Easy,   easySettings   },
        { Difficulty.Medium, mediumSettings },
        { Difficulty.Hard,   hardSettings   }
    };
}
