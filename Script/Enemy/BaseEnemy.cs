using UnityEngine;
using BarthaSzabolcs.Tutorial_SpriteFlash; // Make sure to include the namespace for SimpleFlash

public class BaseEnemy : MonoBehaviour
{
    public int health;
    public int damage;

    public delegate void DeathEventHandler(GameObject enemy);
    public event DeathEventHandler OnDeath;

    private SimpleFlash simpleFlash; // Reference to the SimpleFlash component

    protected virtual void Start()
    {
        // Ensure the enemy has the tag "Enemy"
        gameObject.tag = "Enemy";

        // Get the SimpleFlash component attached to this GameObject
        simpleFlash = GetComponent<SimpleFlash>();

        // Optionally, add a check to ensure the SimpleFlash component is present
        if (simpleFlash == null)
        {
            Debug.LogWarning("SimpleFlash component is missing from the enemy.");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Trigger the flash effect when taking damage
        if (simpleFlash != null)
        {
            simpleFlash.Flash();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add a kill count when the enemy dies
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddKill();
        }

        // Call the OnDeath event before destroying the object
        if (OnDeath != null)
        {
            OnDeath(gameObject);
        }

        // Destroy the enemy object
        Destroy(gameObject);
    }
}
