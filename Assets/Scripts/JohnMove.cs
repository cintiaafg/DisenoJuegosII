using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float Speed = 8f;
    public float JumpForce = 5f;

    [Header("Disparo")]
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public Animator Animator;

    private Rigidbody2D rb;
    private float horizontal;
    private bool Grounded;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        if (Animator != null)
        {
            Animator.speed = 1.5f;
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Animator != null)
        {
            Animator.SetBool("running", horizontal != 0f);
        }

        Grounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            0.1f
        );

        Debug.DrawRay(
            transform.position,
            Vector2.down * 0.1f,
            Color.red
        );

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (horizontal > 0 && !facingRight)
        {
            facingRight = true;
            Flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            facingRight = false;
            Flip();
        }

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
        if (BulletPrefab == null || FirePoint == null)
        {
            Debug.LogWarning("Asigna BulletPrefab y FirePoint.");
            return;
        }

        GameObject bullet = Instantiate(
            BulletPrefab,
            FirePoint.position,
            Quaternion.identity
        );

        BulletScript bulletScript =
            bullet.GetComponent<BulletScript>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(
                facingRight ? Vector2.right : Vector2.left
            );
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;

        scale.x = facingRight
            ? Mathf.Abs(scale.x)
            : -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}
