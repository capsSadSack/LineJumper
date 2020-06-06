using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isOnLeftBorder = false;
    private bool isJumping = false;
    private bool isAggressive = true;

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
            Vector2 direction = GetRandomDirection();
            float velocityModule = GetRandomVelocity();

            rb.velocity = direction * velocityModule;

            isJumping = true;
            anim.SetBool("isJumping", true);

            isAggressive = GetAggression();
            anim.SetBool("isAggressive", isAggressive);

            if (isAggressive)
            {
                Debug.Log("AGGRESSIVE");
            }
            else
            {
                Debug.Log("NOT AGGRESSIVE");
            }
        }
    }

    private Vector2 GetRandomDirection()
    {
        float angleFromVertical_Degree = UnityEngine.Random.Range(50, 130);
        float angleFromVertical_Radian = angleFromVertical_Degree * Mathf.Deg2Rad;
        float x = Mathf.Sin(angleFromVertical_Radian);
        float y = Mathf.Cos(angleFromVertical_Radian);

        if (!isOnLeftBorder)
        {
            x *= -1;
        }

        return new Vector2(x, y);
    }

    private float GetRandomVelocity()
    {
        return UnityEngine.Random.Range(5, 10);
    }

    private bool GetAggression()
    {
        var value = UnityEngine.Random.value;
        return value > 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            isOnLeftBorder = !isOnLeftBorder;

            Vector2 dpos = new Vector2(0.2f, 0);
            if (isOnLeftBorder)
                dpos *= -1;

            rb.position = rb.position + dpos;

            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;
            isJumping = false;
            anim.SetBool("isJumping", false);

            isAggressive = true;
            anim.SetBool("isAggressive", isAggressive);
        }
    }
}
