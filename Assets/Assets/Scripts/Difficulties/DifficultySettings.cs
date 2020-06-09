namespace Assets.Assets.Scripts.Difficulties
{
    public struct DifficultySettings
    {
        public int InitialEnemiesCount { get; set; }
        public float EnemiesSpawnPeriod_Sec { get; set; }
        public float GameActionPeriod_Sec { get; set; }

        public float MaxVelocity { get; set; }
        public float MinVelocity { get; set; }
    }
}
