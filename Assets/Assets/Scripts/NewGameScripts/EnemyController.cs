using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool IsAggressive { get; private set; } = true;

    private bool isOnLeftBorder = false;
    private bool isJumping = true;

    private Rigidbody2D rb;
    private Animator anim;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                    Debug.Log("AGGRESSIVE");
                }
                else
                {
                    Debug.Log("NOT AGGRESSIVE");
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
        if (currentPosition.y > 3)
        {
            return 0;
        }
        else if (currentPosition.y < 0)
        {
            return 50;
        }
        else
        {
            float topLimitY = 90 - 40 * (3 - currentPosition.y) / 3.0f;
            return topLimitY;
        }
    }

    private float GetBottomLimitY(Vector2 currentPosition)
    {
        if (currentPosition.y < -3)
        {
            return 0;
        }
        else if (currentPosition.y > 0)
        {
            return 130;
        }
        else
        {
            float bottomLimitY = 180 - 40 * (3 - currentPosition.y) / 3.0f;
            return bottomLimitY;
        }
    }

    private float GetRandomVelocity()
    {
        return UnityEngine.Random.Range(3, 8);
    }

    private bool GetAggression()
    {
        var value = UnityEngine.Random.value;
        return value > 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Border"))
        {
            isOnLeftBorder = collider.gameObject.transform.position.x < 0;

            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;

            isJumping = false;
            IsAggressive = true;
            anim.SetBool("isAggressive", IsAggressive);
        }
    }
}
