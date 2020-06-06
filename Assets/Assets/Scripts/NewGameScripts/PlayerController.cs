using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnPlayerMove;
    public UnityEvent OnGoodEnemyCollision;
    public UnityEvent OnAggressiveEnemyCollision;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            Vector3 direction = (mouseWorldPosition - transform.position);

            rb.velocity = new Vector2(direction.x, direction.y);

            OnPlayerMove.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy!");
            OnGoodEnemyCollision.Invoke();
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("Border!");
            rb.velocity = new Vector2(0, 0);
        }
    }
}
