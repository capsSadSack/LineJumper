using Assets.Assets.Scripts.Difficulties;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameAction;
    public GameObject enemiesTransformParent;
    public ScoreController scoreController;
    public PlayerController player;

    private SimpleTimer spawningTimer;
    private SimpleTimer gameActionTimer;
    private SimpleTimer pickUpTimer;

    private Difficulty currentDifficulty;
    private EnemySpawner enemySpawner;
    private PickUpsSpawner pickUpsSpawner;

    private const float pickUpSpawnPeriod_Sec = 20.0f;

    private void Start()
    {
        PlayerPrefsDifficultyAccess difficultyAccess = new PlayerPrefsDifficultyAccess();
        currentDifficulty = difficultyAccess.GetDifficulty();

        DifficultySettings difficultySettings = DifficultiesSettingsStorage.Settings[currentDifficulty];

        enemySpawner = new EnemySpawner(enemiesTransformParent, OnGameAction, scoreController.IncrementScore);
        pickUpsSpawner = new PickUpsSpawner(enemiesTransformParent, this, player);

        spawningTimer = new SimpleTimer(difficultySettings.EnemiesSpawnPeriod_Sec, enemySpawner.SpawnNewEnemy);
        gameActionTimer = new SimpleTimer(difficultySettings.GameActionPeriod_Sec, OnGameAction.Invoke);
        pickUpTimer = new SimpleTimer(pickUpSpawnPeriod_Sec, pickUpsSpawner.SpawnPickUp);

        enemySpawner.CreateInitialEnemies(difficultySettings.InitialEnemiesCount);
    }

    private void Update()
    {
        spawningTimer.UpdateTimer();
        gameActionTimer.UpdateTimer();
        pickUpTimer.UpdateTimer();
    }

    public void DestroyAllEnemies()
    {
        var enemies = GetAllEnemies();

        foreach (var enemy in enemies)
        {
            var ec = enemy.GetComponent<EnemyController>();
            ec.Explode();
        }
    }

    private IEnumerable<GameObject> GetAllEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();

        for (int i = 0; i < enemiesTransformParent.transform.childCount; i++)
        {
            var probablyEnemy = enemiesTransformParent.transform.GetChild(i).gameObject;

            if (probablyEnemy.CompareTag("Enemy"))
            {
                enemies.Add(probablyEnemy);
            }
        }

        return enemies;
    }

}
