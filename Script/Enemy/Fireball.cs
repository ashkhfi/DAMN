using UnityEngine;
using UI; // Assuming Movement script is in the UI namespace

public class Fireball : MonoBehaviour
{
    public float speed = 10f;          // Speed of the fireball
    public int damage = 20;            // Damage caused by the fireball
    public GameObject explosionEffect; // Effect to play on explosion
    public float lifetime = 5f;        // Time before fireball self-destructs
    public Transform target;           // Target to track (e.g., player)
    public float deviationAmount = 1f; // Amount of deviation in movement direction

    private Vector3 initialDirection;   // Initial direction towards the target
    private float randomDeviationAngle; // Random angle for deviation

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy fireball after specified lifetime

        if (target != null)
        {
            initialDirection = (target.position - transform.position).normalized;
            randomDeviationAngle = Random.Range(-deviationAmount, deviationAmount);
        }
        else
        {
            initialDirection = transform.forward; // Fallback direction if no target
        }
    }

    private void Update()
    {
        Vector3 targetDirection = initialDirection;

        // Add random deviation to the movement direction
        Vector3 randomDeviation = Quaternion.AngleAxis(randomDeviationAngle, Vector3.up) * initialDirection;
        targetDirection += randomDeviation.normalized * Random.Range(0f, deviationAmount);

        // Move towards the adjusted direction
        transform.Translate(targetDirection * speed * Time.deltaTime);

        // Destroy the fireball if no target and reaches end of life
        if (target == null)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
            {
                Explode();
            }
        }
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fireball collided with: " + collision.gameObject.name);

        // Check if the collided object has the Movement script
        Movement movement = collision.GetComponent<Movement>();
        if (movement != null)
        {
            HealthBar healthBar = movement.healthBar;
            if (healthBar != null)
            {
                healthBar.TakeDamage(damage);
                Debug.Log("Player health after taking damage: " + healthBar.currentHealth);
            }
            Explode();
        }
        else
        {
            Debug.LogError("Movement script not found on collided object: " + collision.gameObject.name);
        }

        // Destroy the fireball on impact
        Explode();
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation); // Instantiate explosion effect
        }

        Destroy(gameObject); // Destroy the fireball
    }


}

