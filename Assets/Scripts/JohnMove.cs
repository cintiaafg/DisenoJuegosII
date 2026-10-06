using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpForce = 7f;

    [Header("Disparo")]
    public GameObject BulletPrefab;
    public Transform FirePoint;

    private Rigidbody2D rb;
    private float horizontal;
    private bool Grounded;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Debug.DrawRay(
            transform.position,
            Vector2.down * 0.1f,
            Color.red
        );

        if (Physics2D.Raycast(
            transform.position,
            Vector2.down,
            0.1f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

        // Saltar
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        // Dirección de John
        if (horizontal > 0)
        {
            facingRight = true;
            Flip();
        }
        else if (horizontal < 0)
        {
            facingRight = false;
            Flip();
        }

        // Disparar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            horizontal * Speed,
            rb.linearVelocity.y
        );
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            JumpForce
        );
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(
            BulletPrefab,
            FirePoint.position,
            Quaternion.identity
        );

        BulletScript bulletScript =
            bullet.GetComponent<BulletScript>();

        if (facingRight)
        {
            bulletScript.SetDirection(Vector2.right);
        }
        else
        {
            bulletScript.SetDirection(Vector2.left);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;

        if (facingRight)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}