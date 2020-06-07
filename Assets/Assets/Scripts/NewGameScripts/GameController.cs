using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameAction;

    public float spawnPeriod_Sec;
    public float gameActionPeriod_Sec;
    public int initialEnemiesNumber;

    public GameObject enemiesTransformParent;

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
        float dy = 150;
        float x0 = 460;
        float y0 = 0;

        for (int i = 0; i < enemiesNumber; i++)
        {
            var enemy = CreateEnemy();
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.gameObject.transform.localPosition = new Vector2(x0, y0 + Mathf.Pow(-1, i) * ((i + 1) / 2) * dy );
        }
    }

    private void SpawnNewEnemy()
    {
        var enemy = CreateEnemy();
        GetPosAndVel(out Vector2 position, out Vector2 velocity);

        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        rb.gameObject.transform.localPosition = position;
        rb.velocity = velocity;
    }

    private GameObject CreateEnemy()
    {
        var source = Resources.Load("Prefabs/Enemy");
        GameObject objSource = (GameObject)Instantiate(source);
        objSource.transform.parent = enemiesTransformParent.transform;

        EnemyController enemyController = objSource.GetComponent<EnemyController>();
        OnGameAction.AddListener(() => { enemyController.Jump(); });

        return objSource;
    }

    private void GetPosAndVel(out Vector2 position, out Vector2 velocity)
    {
        float x0 = 0;
        float y0 = 1000;
        Vector2 vel = new Vector2(1, -1.5f);

        bool onTop = UnityEngine.Random.value > 0.5;

        if (!onTop)
        {
            y0 *= -1;
            vel *= -1;
        }

        position = new Vector2(x0, y0);
        velocity = vel;
    }

    public void Victory()
    {
        int score = scoreController.Score;
        // SaveScore
        // Check Achievements
    }
}
