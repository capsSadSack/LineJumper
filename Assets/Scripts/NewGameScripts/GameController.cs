using Assets.Assets.Scripts.Difficulties;
using Assets.Scripts.NewGameScripts;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemiesTransformParent;
    public ScoreController scoreController;
    public PlayerController player;

    private DecreasingPeriodTimer spawningTimer;
    private SimpleTimer gameActionTimer;
    private SimpleTimer pickUpTimer;

    private Difficulty currentDifficulty;
    private EnemySpawner enemySpawner;
    private PickUpsSpawner pickUpsSpawner;

    private const float pickUpSpawnPeriod_Sec = 3.0f;

    private void Start()
    {
        PlayerPrefsDifficultyAccess difficultyAccess = new PlayerPrefsDifficultyAccess();
        currentDifficulty = difficultyAccess.GetDifficulty();

        DifficultySettings difficultySettings = DifficultiesSettingsStorage.Settings[currentDifficulty];

        enemySpawner = new EnemySpawner(enemiesTransformParent, scoreController.IncrementScore);
        pickUpsSpawner = new PickUpsSpawner(enemiesTransformParent, this, player);

        spawningTimer = new DecreasingPeriodTimer(difficultySettings.InitialEnemiesSpawnPeriod_Sec, difficultySettings.MinimumEnemiesSpawnPeriod_Sec, 0.1f, enemySpawner.SpawnNewEnemy);
        gameActionTimer = new SimpleTimer(difficultySettings.GameActionPeriod_Sec, MoveEnemies);
        pickUpTimer = new SimpleTimer(pickUpSpawnPeriod_Sec, pickUpsSpawner.SpawnPickUp);

        enemySpawner.CreateInitialEnemies(difficultySettings.InitialEnemiesCount);
    }

    private void Update()
    {
        spawningTimer.UpdateTimer();
        gameActionTimer.UpdateTimer();
        pickUpTimer.UpdateTimer();
    }

    private void MoveEnemies()
    {
        foreach (var enemy in enemySpawner.Enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyController>().Jump();
            }
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (var enemy in enemySpawner.Enemies)
        {
            if (enemy != null)
            {
                var ec = enemy.GetComponent<EnemyController>();
                ec.Explode();
            }
        }
    }
}
