using UnityEngine;
using UnityEngine.Events;

public class NuclearBombBehaviour : MonoBehaviour
{
    public UnityEvent OnNuclearBombExplode;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(0, -1);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameObject.Destroy(this.gameObject);
            OnNuclearBombExplode.Invoke();
        }
    }
}
