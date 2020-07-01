namespace Assets.Assets.Scripts.Difficulties
{
    public struct DifficultySettings
    {
        public int InitialEnemiesCount { get; set; }
        public float InitialEnemiesSpawnPeriod_Sec { get; set; }
        public float MinimumEnemiesSpawnPeriod_Sec { get; set; }
        public float GameActionPeriod_Sec { get; set; }

        public float MaxVelocity { get; set; }
        public float MinVelocity { get; set; }
    }
}
