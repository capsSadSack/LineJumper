using Assets.Assets.Scripts.Difficulties;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AudioSource enemyFlightAudio;
    public AudioSource borderCollideAudio;

    public bool IsAggressive { get; private set; } = true;

    private bool isOnLeftBorder = false;
    private bool isJumping = true;

    private Rigidbody2D rb;
    private Animator anim;

    private float maxVelocity;
    private float minVelocity;

    private IAggressionController aggressionController = new AggressionController();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        PlayerPrefsDifficultyAccess difficultyAccess = new PlayerPrefsDifficultyAccess();
        var difficulty = difficultyAccess.GetDifficulty();

        DifficultySettings difficultySettings = DifficultiesSettingsStorage.Settings[difficulty];

        maxVelocity = difficultySettings.MaxVelocity;
        minVelocity = difficultySettings.MinVelocity;
    }

    public void Explode()
    {
        anim.SetBool("isExploding", true);
        


    }

    public void Jump()
    {
        if (!isJumping)
        {
            if (UnityEngine.Random.value > 0.66)
            {
                Vector2 coordinates = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Vector2 direction = GetRandomDirection(coordinates);
                float velocityModule = GetRandomVelocity();

                rb.velocity = direction * velocityModule;
                rb.angularVelocity = 360;
                isJumping = true;

                IsAggressive = GetAggression();
                anim.SetBool("isAggressive", IsAggressive);

                if (IsAggressive)
                {
                    enemyFlightAudio.Play();
                }
            }
        }
    }

    private Vector2 GetRandomDirection(Vector2 currentPosition)
    {
        float topLimitY = GetTopLimitY(currentPosition);
        float bottomLimitY = GetBottomLimitY(currentPosition);

        float angleFromVertical_Degree = UnityEngine.Random.Range(topLimitY, bottomLimitY);
        float angleFromVertical_Radian = angleFromVertical_Degree * Mathf.Deg2Rad;
        float x = Mathf.Sin(angleFromVertical_Radian);
        float y = Mathf.Cos(angleFromVertical_Radian);

        if (!isOnLeftBorder)
        {
            x *= -1;
        }

        return new Vector2(x, y);
    }

    private float GetTopLimitY(Vector2 currentPosition)
    {
        float x1 = 0;
        float y1 = 50;

        float x2 = 4;
        float y2 = 90;

        if (currentPosition.y > x2)
        {
            return y2;
        }
        else if (currentPosition.y < x1)
        {
            return y1;
        }
        else
        {
            float topLimitY = y1 + (y1 - y2) / (x1 - x2) * currentPosition.y;
            return topLimitY;
        }
    }

    private float GetBottomLimitY(Vector2 currentPosition)
    {
        float x1 = 0;
        float y1 = 130;

        float x2 = -4;
        float y2 = 90;

        if (currentPosition.y < x2)
        {
            return y2;
        }
        else if (currentPosition.y > x1)
        {
            return y1;
        }
        else
        {
            float bottomLimitY = y1 + (y1 - y2) / (x1 - x2) * currentPosition.y;
            return bottomLimitY;
        }
    }

    private float GetRandomVelocity()
    {
        return UnityEngine.Random.Range(minVelocity, maxVelocity);
    }

    private bool GetAggression()
    {
        Aggression aggression = aggressionController.GetAggression();
        return aggression == Aggression.Aggressive;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Border"))
        {
            enemyFlightAudio.Stop();
            borderCollideAudio.Play();

            isOnLeftBorder = collider.gameObject.transform.position.x < 0;

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
