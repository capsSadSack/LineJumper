using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnPlayerMove;
    public UnityEvent OnGoodEnemyCollision;
    public UnityEvent OnAggressiveEnemyCollision;

    private Rigidbody2D rb;
    private bool isJumping = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
            Debug.Log("Enemy!");

            var enemyController = collider.gameObject.GetComponent<EnemyController>();

            if (enemyController.IsAggressive)
            {
                OnAggressiveEnemyCollision.Invoke();
            }
            else
            {
                GameObject.Destroy(collider.gameObject);
                OnGoodEnemyCollision.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            rb.velocity = new Vector2(0, 0);
            isJumping = false;
        }
    }
}
