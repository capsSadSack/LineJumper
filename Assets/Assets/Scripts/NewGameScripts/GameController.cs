using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameAction;

    public float spawnPeriod_Sec;
    public float gameActionPeriod_Sec;
    public int initialEnemiesNumber;

    private SimpleTimer spawningTimer;
    private SimpleTimer gameActionTimer;
    private ScoreController scoreController;


    private void Start()
    {
        scoreController = GetComponent<ScoreController>();

        spawningTimer = new SimpleTimer(spawnPeriod_Sec, SpawnNewEnemy);
        gameActionTimer = new SimpleTimer(gameActionPeriod_Sec, OnGameAction.Invoke);
        CreateEnemies(initialEnemiesNumber);
    }

    private void Update()
    {
        spawningTimer.UpdateTimer();
        gameActionTimer.UpdateTimer();
    }

    private void CreateEnemies(int enemiesNumber)
    {
        for (int i = 0; i < enemiesNumber; i++)
        {
            var enemy = CreateEnemy();
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.position = new Vector2(0, 0);
            rb.velocity = new Vector2(1, 1);
        }
    }

    private void SpawnNewEnemy()
    {
        var enemy = CreateEnemy();
        GetPosAndVel(out Vector2 position, out Vector2 velocity);

        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        rb.position = position;
        rb.velocity = velocity;
    }

    private GameObject CreateEnemy()
    {
        var source = Resources.Load("Prefabs/Enemy");
        GameObject objSource = (GameObject)Instantiate(source);

        EnemyController enemyController = objSource.GetComponent<EnemyController>();
        OnGameAction.AddListener(() => { enemyController.Jump(); });

        return objSource;
    }

    private void GetPosAndVel(out Vector2 position, out Vector2 velocity)
    {
        bool onTop = UnityEngine.Random.value > 0.5;

        position = (onTop) ? new Vector2(0, 4) : new Vector2(0, -4);
        velocity = (onTop) ? new Vector2(1, -1) : new Vector2(1, 1);
    }

    public void Victory()
    {
        int score = scoreController.Score;
        // SaveScore
        // Check Achievements
    }
}
