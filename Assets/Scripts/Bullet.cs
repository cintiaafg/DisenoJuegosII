using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    private Rigidbody2D rb;
    private Vector2 direction = Vector2.right;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }
}
