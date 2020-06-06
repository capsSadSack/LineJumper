using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameAction;

    public float spawnPeriod_Sec;
    public float gameActionPeriod_Sec;
    public int initialEnemiesNumber;

    private DateTime lastSpawnTime;
    private DateTime lastGameActionTime;
    private ScoreController scoreController;


    private void Start()
    {
        scoreController = GetComponent<ScoreController>();

        CreateEnemies(initialEnemiesNumber);
        lastSpawnTime = DateTime.UtcNow;

    }

    private void Update()
    {
        if ((DateTime.UtcNow - lastSpawnTime).TotalSeconds >= spawnPeriod_Sec)
        {
            SpawnNewEnemy();
            lastSpawnTime = DateTime.UtcNow;
        }

        if ((DateTime.UtcNow - lastGameActionTime).TotalSeconds >= gameActionPeriod_Sec)
        {
            lastGameActionTime = DateTime.UtcNow;
            OnGameAction.Invoke();
        }
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
