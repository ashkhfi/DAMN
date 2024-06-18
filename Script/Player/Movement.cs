using UnityEngine;
using BarthaSzabolcs.Tutorial_SpriteFlash; // Make sure to include the namespace for SimpleFlash

namespace UI
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Movement : MonoBehaviour
    {
        public float moveSpeed = 5f; // Horizontal movement speed
        public float jumpForce = 10f; // Force applied for jumping

        private Rigidbody2D rb;
        private Animator animator;
        private bool idle;

        // Reference to the player's HealthBar
        public HealthBar healthBar;

        // Reference to the SimpleFlash component
        private SimpleFlash simpleFlash;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            // If healthBar is not set in the Inspector, find it automatically
            if (healthBar == null)
            {
                healthBar = FindObjectOfType<HealthBar>();
                if (healthBar == null)
                {
                    Debug.LogError("HealthBar is not assigned and cannot be found in the scene!");
                }
            }

            // Get the SimpleFlash component attached to this GameObject
            simpleFlash = GetComponent<SimpleFlash>();

            // Optionally, add a check to ensure the SimpleFlash component is present
            if (simpleFlash == null)
            {
                Debug.LogWarning("SimpleFlash component is missing from the player.");
            }
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

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                idle = true;
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                // Get the BaseEnemy script from the collided enemy
                BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

                if (enemy != null && healthBar != null)
                {
                    healthBar.TakeDamage(enemy.damage);

                    // Trigger the flash effect when taking damage
                    if (simpleFlash != null)
                    {
                        simpleFlash.Flash();
                    }
                }
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                idle = false;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // Example: Triggered by fireball
            if (collision.gameObject.CompareTag("Fireball"))
            {
                Fireball fireball = collision.gameObject.GetComponent<Fireball>();

                if (fireball != null && healthBar != null)
                {
                    healthBar.TakeDamage(fireball.damage);

                    // Trigger the flash effect when hit by fireball
                    if (simpleFlash != null)
                    {
                        simpleFlash.Flash();
                    }
                }
            }
        }
    }
}
