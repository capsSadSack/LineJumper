using Assets.Assets.Scripts.Difficulties;
using Assets.Scripts.NewGameScripts.Enemy;
using Assets.Scripts.NewGameScripts.Enemy.AggressionChanging;
using Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public Enemy EnemyType => enemy.EnemyType;
    public Action onEnemyDestoroyed;

    public AudioSource enemyFlightAudio;
    public AudioSource borderCollideAudio;

    private EnemyFabric enemyFabric;

    public bool IsAggressive { get; private set; } = true;

    private bool isJumping = true;

    private Rigidbody2D rb;
    private Animator anim;

    private AEnemy enemy;


    private void Awake()
    {
        enemyFabric = GameObject.Find("GameFieldPanel").GetComponent<EnemyFabric>();
        enemy = enemyFabric.CreateEnemy(Enemy.Simple);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetEnemyType(Enemy enemy)
    {
        this.enemy = enemyFabric.CreateEnemy(enemy);
    }

    public void Explode()
    {
        anim.SetBool("isExploding", true);
    }

    public void DestroyEnemy()
    {
        onEnemyDestoroyed();
        GameObject.Destroy(this.gameObject);
    }

    public void Jump()
    {
        if (gameObject != null) // TODO: [CG, 2020.07.11] заглушка: не заходит дальше, если объект уничтожен. Однако, этот метод в таком случае вызываться вообще не должен.
        {
            if (!isJumping && enemy.IsGoingToJump())
            {
                Vector2 coordinates = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

                Vector2 direction = enemy.GetJumpDirection(coordinates);
                float velocityModule = enemy.GetVelocityMagnitude();

                rb.velocity = direction * velocityModule;
                rb.angularVelocity = 360;
                isJumping = true;

                IsAggressive = enemy.GetAggression();
                anim.SetBool("isAggressive", IsAggressive);

                if (IsAggressive)
                {
                    enemyFlightAudio.Play();
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Border"))
        {
            enemyFlightAudio.Stop();
            borderCollideAudio.Play();

            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;

            isJumping = false;
            IsAggressive = true;
            anim.SetBool("isAggressive", IsAggressive);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyFlightAudio.Stop();
        }
    }
}
