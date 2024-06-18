using UnityEngine;

namespace Enemy
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

        // Referensi ke HealthBar player
        public HealthBar healthBar;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            // Jika healthBar belum di-set di Inspector, cari secara otomatis
            if (healthBar == null)
            {
                healthBar = FindObjectOfType<HealthBar>();
                if (healthBar == null)
                {
                    Debug.LogError("HealthBar is not assigned and cannot be found in the scene!");
                }
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
                // Mendapatkan script BaseEnemy dari musuh yang bertabrakan
                BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

                if (enemy != null && healthBar != null)
                {
                    healthBar.TakeDamage(enemy.damage);
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
    }

}
    