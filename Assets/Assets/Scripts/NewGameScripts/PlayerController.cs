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

    private ResetableTimer stayStillTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stayStillTimer = new ResetableTimer(30, () => { UnlockAchievement(Achievement.StayStill); });
        stayStillTimer.Start();
    }


    private void Update()
    {
        stayStillTimer.UpdateTimer();

        if (!isJumping && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            Vector3 direction = (mouseWorldPosition - transform.position);

            rb.velocity = 10 * new Vector2(direction.x, direction.y).normalized;
            isJumping = true;
            stayStillTimer.Stop();

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
                        UnlockAchievement(Achievement.DoubleKill);
                    }
                }
                else
                {
                    UnlockAchievement(Achievement.WorkAndRest);
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
            stayStillTimer.Start();
        }
    }

    private void UnlockAchievement(Achievement achievement)
    {
        AchievementUnlockedArgs args = new AchievementUnlockedArgs()
        {
            Achievement = achievement
        };

        OnAchievementUnlocked.Invoke(args);
    }
}
