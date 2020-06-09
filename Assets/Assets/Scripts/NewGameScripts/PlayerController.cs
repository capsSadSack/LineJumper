using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnPlayerMove;
    public UnityEvent OnGoodEnemyCollision;
    public UnityEvent OnAggressiveEnemyCollision;
    public AchievementUnlockedEvent OnAchievementUnlocked;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private int enemiesDestroyedInSingleJump = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (!isJumping && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            Vector3 direction = (mouseWorldPosition - transform.position);

            rb.velocity = 10 * new Vector2(direction.x, direction.y).normalized;
            isJumping = true;

            OnPlayerMove.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            var enemyController = collider.gameObject.GetComponent<EnemyController>();

            if (enemyController.IsAggressive)
            {
                OnAggressiveEnemyCollision.Invoke();
            }
            else
            {
                if (isJumping)
                {
                    enemiesDestroyedInSingleJump++;
                    if (enemiesDestroyedInSingleJump >= 2)
                    {
                        UnlockDoubleKillAchievement();
                    }
                }
                else
                {
                    enemiesDestroyedInSingleJump = 0;
                }

                GameObject.Destroy(collider.gameObject);
                OnGoodEnemyCollision.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            enemiesDestroyedInSingleJump = 0;

            rb.velocity = new Vector2(0, 0);
            isJumping = false;
        }
    }

    private void UnlockDoubleKillAchievement()
    {
        AchievementUnlockedArgs args = new AchievementUnlockedArgs()
        {
            Achievement = Achievement.DoubleKill
        };

        OnAchievementUnlocked.Invoke(args);
    }
}
