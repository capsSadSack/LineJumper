using Assets.Scripts.NewGameScripts.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner
{
    public List<GameObject> Enemies { get; set; } = new List<GameObject>();

    private GameObject enemiesTransformParent;
    private Action onEnemyDestroyed;


    public EnemySpawner(GameObject enemiesTransformParent, Action onEnemyDestroyed)
    {
        this.enemiesTransformParent = enemiesTransformParent;
        this.onEnemyDestroyed = onEnemyDestroyed;
    }


    public void CreateInitialEnemies(int enemiesNumber)
    {
        float dy = 150;
        float x0 = 450;
        float y0 = 0;

        for (int i = 0; i < enemiesNumber; i++)
        {
            var enemy = CreateEnemy(Enemy.Simple);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.gameObject.transform.localPosition = new Vector2(x0, y0 + Mathf.Pow(-1, i) * ((i + 1) / 2) * dy);
            rb.velocity = new Vector2(1, 0);
            rb.angularVelocity = 360;

            Enemies.Add(enemy);
        }
    }

    public void SpawnNewEnemy()
    {
        var enemy = CreateEnemy();

        GetPosAndVel(out Vector2 position, out Vector2 velocity);

        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        rb.gameObject.transform.localPosition = position;
        rb.velocity = velocity;
        rb.angularVelocity = 360;

        Enemies.Add(enemy);
    }

    private GameObject CreateEnemy(Enemy enemyType)
    {
        GameObject objSource = GetEnemy(enemyType);

        EnemyController enemyController = objSource.GetComponent<EnemyController>();
        enemyController.SetEnemyType(enemyType);
        enemyController.onEnemyDestoroyed = onEnemyDestroyed;

        return objSource;
    }

    private GameObject CreateEnemy()
    {
        Enemy enemyType = GetEnemyType();
        return CreateEnemy(enemyType);
    }

    private Enemy GetEnemyType()
    {
        if (UnityEngine.Random.value < 0) //>= 0.95)
        {
            return Enemy.Immortal;
        }
        else if (UnityEngine.Random.value > 0)//> 0.8)
        {
            return Enemy.Follower;
        }
        else
        {
            return Enemy.Simple;
        }
    }

    private GameObject GetEnemy(Enemy enemyType)
    {
        UnityEngine.Object source;

        switch (enemyType)
        {
            case Enemy.Simple:
                {
                    source = Resources.Load("Prefabs/Enemy");
                    return GameObject.Instantiate(source, enemiesTransformParent.transform, false) as GameObject;
                }
            case Enemy.Follower:
                {
                    source = Resources.Load("Prefabs/FollowingEnemy");
                    return GameObject.Instantiate(source, enemiesTransformParent.transform, false) as GameObject;
                }
            case Enemy.Immortal:
                {
                    source = Resources.Load("Prefabs/ImmortalEnemy");
                    var enemy = GameObject.Instantiate(source, enemiesTransformParent.transform, false) as GameObject;

                    var auraSource = Resources.Load("Prefabs/EnemyAura");
                    var aura = GameObject.Instantiate(auraSource, enemiesTransformParent.transform, false) as GameObject;
                    aura.transform.position = enemy.transform.position;
                    aura.GetComponent<EnemyAuraController>().objectToFollow = enemy;
                    aura.transform.localScale = new Vector3(35, 35, 1);

                    return enemy;
                }
            default:
                {
                    source = Resources.Load("Prefabs/Enemy");
                    return GameObject.Instantiate(source, enemiesTransformParent.transform, false) as GameObject;
                }
        }
    }

    private void GetPosAndVel(out Vector2 position, out Vector2 velocity)
    {
        float x0 = 0;
        float y0 = 1000;
        Vector2 vel = new Vector2(1 + UnityEngine.Random.value * 0.5f, -1 + UnityEngine.Random.value * 0.5f);

        bool onTop = UnityEngine.Random.value > 0.5;

        if (!onTop)
        {
            y0 *= -1;
            vel *= -1;
        }

        position = new Vector2(x0, y0);
        velocity = vel;
    }
}

