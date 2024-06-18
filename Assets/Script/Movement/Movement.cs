using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Horizontal movement speed
    public float jumpForce = 10f; // Force applied for jumping

    private Rigidbody2D rb;
    private Animator animator;
    private bool idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Handle horizontal movement
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && idle)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        // Flip the sprite based on the movement direction
        if (moveX > 0)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }

        // Update the animator parameters
        animator.SetFloat("run", Mathf.Abs(moveX));
        animator.SetBool("idle", idle);
    }

    // Check if the player is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            idle = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            idle = false;
        }
    }
}
