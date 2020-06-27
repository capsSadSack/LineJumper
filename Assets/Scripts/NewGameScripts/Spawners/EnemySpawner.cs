﻿using System;
using UnityEngine;
using UnityEngine.Events;

// NOTE: Хоть класс и MonoBehaviour, его не надо прицеплять и GameObject.
public class EnemySpawner : MonoBehaviour
{
    private GameObject enemiesTransformParent;
    private UnityEvent onGameAction;
    private Action onEnemyDestroyed;

    public EnemySpawner(GameObject enemiesTransformParent, UnityEvent onGameAction, Action onEnemyDestroyed)
    {
        this.enemiesTransformParent = enemiesTransformParent;
        this.onGameAction = onGameAction;
        this.onEnemyDestroyed = onEnemyDestroyed;
    }

    public void CreateInitialEnemies(int enemiesNumber)
    {
        float dy = 150;
        float x0 = 450;
        float y0 = 0;

        for (int i = 0; i < enemiesNumber; i++)
        {
            var enemy = CreateEnemy();
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.gameObject.transform.localPosition = new Vector2(x0, y0 + Mathf.Pow(-1, i) * ((i + 1) / 2) * dy);
            rb.velocity = new Vector2(1, 0);
            rb.angularVelocity = 360;
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
    }

    private GameObject CreateEnemy()
    {
        var source = Resources.Load("Prefabs/Enemy");
        GameObject objSource = (GameObject)Instantiate(source);
        objSource.transform.parent = enemiesTransformParent.transform;

        EnemyController enemyController = objSource.GetComponent<EnemyController>();
        enemyController.onEnemyDestoroyed = onEnemyDestroyed;
        onGameAction.AddListener(() => { enemyController.Jump(); });

        return objSource;
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

