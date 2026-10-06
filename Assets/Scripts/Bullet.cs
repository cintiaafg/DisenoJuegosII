using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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