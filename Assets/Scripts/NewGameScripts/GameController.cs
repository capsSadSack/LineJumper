using Assets.Assets.Scripts.Difficulties;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameAction;

    public GameObject enemiesTransformParent;

    private SimpleTimer spawningTimer;
    private SimpleTimer gameActionTimer;

    private Difficulty currentDifficulty;
    private EnemySpawner enemySpawner;
    private PickUpsSpawner pickUpsSpawner;

    private void Start()
    {
        PlayerPrefsDifficultyAccess difficultyAccess = new PlayerPrefsDifficultyAccess();
        currentDifficulty = difficultyAccess.GetDifficulty();

        DifficultySettings difficultySettings = DifficultiesSettingsStorage.Settings[currentDifficulty];

        enemySpawner = new EnemySpawner(enemiesTransformParent, OnGameAction);
        pickUpsSpawner = new PickUpsSpawner(enemiesTransformParent);

        spawningTimer = new SimpleTimer(difficultySettings.EnemiesSpawnPeriod_Sec, enemySpawner.SpawnNewEnemy);
        gameActionTimer = new SimpleTimer(difficultySettings.GameActionPeriod_Sec, OnGameAction.Invoke);

        enemySpawner.CreateInitialEnemies(difficultySettings.InitialEnemiesCount);
    }

    private void Update()
    {
        spawningTimer.UpdateTimer();
        gameActionTimer.UpdateTimer();
    }


}
