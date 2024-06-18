using UnityEngine;

public class BanasEnemy : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballCooldown = 2f;

    private Transform playerTransform; // Reference to player's Transform
    private float currentCooldown = 0f; // Current cooldown timer

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player and get their Transform
    }

    private void Update()
    {
        if (playerTransform == null)
            return; // If player is not found, do nothing

        // Update the current cooldown timer
        currentCooldown -= Time.deltaTime;

        // Check if enemy can shoot (cooldown has elapsed)
        if (CanShoot())
        {
            ShootFireball();
            currentCooldown = fireballCooldown; // Reset cooldown timer
        }
    }

    private bool CanShoot()
    {
        // Check if cooldown has elapsed
        return currentCooldown <= 0f;
    }

    private void ShootFireball()
    {
        // Instantiate a fireball prefab at the fire point position and rotation
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.target = playerTransform; // Set the fireball's target to the player's Transform
        }
    }
}
