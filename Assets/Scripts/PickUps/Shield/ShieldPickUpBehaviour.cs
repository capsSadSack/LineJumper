using Shield;
using UnityEngine;

public class ShieldPickUpBehaviour : MonoBehaviour
{
    public ShieldPickedUpEvent OnShieldPickedUp;

    public Sprite shield01;
    public Sprite shield02;
    public Sprite shield03;
    public SpriteRenderer sr;

    private Vector3 initialPosition;
    private int shieldLevel;

    private float dy = 0.7f;
    private float xAmplitude = 1.0f;
    private float t = 0;

    private void Start()
    {
        initialPosition = gameObject.transform.position;
        if (initialPosition.y > 0)
        {
            dy *= -1;
        }

        sr = gameObject.GetComponent<SpriteRenderer>();

        UpdateSprite();
    }

    public void SetShieldLevel(int value)
    {
        shieldLevel = value;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        Sprite[] sprites = new Sprite[] { shield01, shield02, shield03 };

        if (shieldLevel > 0 && shieldLevel <= 3)
        {
            sr.sprite = sprites[shieldLevel - 1];
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
        float y = initialPosition.y + t * dy;
        float x = initialPosition.x + xAmplitude * Mathf.Sin(2 * Mathf.PI * 0.2f * t);

        gameObject.transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameObject.Destroy(this.gameObject);
            OnShieldPickedUp.Invoke(shieldLevel);
        }
    }
}
