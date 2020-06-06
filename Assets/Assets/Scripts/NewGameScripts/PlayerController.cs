using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnPlayerMove;
    public UnityEvent OnGoodEnemyCollision;
    public UnityEvent OnAggressiveEnemyCollision;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            Vector3 direction = (mouseWorldPosition - transform.position);

            rb.velocity = 10 * new Vector2(direction.x, direction.y).normalized;

            OnPlayerMove.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy!");

            var enemyController = collision.gameObject.GetComponent<EnemyController>();

            if (enemyController.IsAggressive)
            {
                OnAggressiveEnemyCollision.Invoke();
            }
            else
            {
                OnGoodEnemyCollision.Invoke();
            }
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("Border!");
            rb.velocity = new Vector2(0, 0);
        }
    }
}
