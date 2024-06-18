using UnityEngine;

public class RajaShoot : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform[] firePoints; // Array of fire points
    public float[] fireballCooldowns; // Array of cooldowns corresponding to each fire point

    private Transform playerTransform; // Reference to player's Transform
    private float[] currentCooldowns; // Current cooldown timers for each fire point

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player and get their Transform
        
        // Initialize the current cooldown timers
        currentCooldowns = new float[firePoints.Length];
        for (int i = 0; i < currentCooldowns.Length; i++)
        {
            currentCooldowns[i] = 0f;
        }
    }

    private void Update()
    {
        if (playerTransform == null)
            return; // If player is not found, do nothing

        for (int i = 0; i < firePoints.Length; i++)
        {
            // Update the current cooldown timer
            currentCooldowns[i] -= Time.deltaTime;

            // Check if the current fire point can shoot (cooldown has elapsed)
            if (CanShoot(i))
            {
                ShootFireball(i);
                currentCooldowns[i] = fireballCooldowns[i]; // Reset cooldown timer for the current fire point
            }
        }
    }

    private bool CanShoot(int firePointIndex)
    {
        // Check if cooldown has elapsed for the given fire point
        return currentCooldowns[firePointIndex] <= 0f;
    }

    private void ShootFireball(int firePointIndex)
    {
        // Instantiate a fireball prefab at the fire point position and rotation
        GameObject fireball = Instantiate(fireballPrefab, firePoints[firePointIndex].position, firePoints[firePointIndex].rotation);
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.target = playerTransform; // Set the fireball's target to the player's Transform
        }
    }
}
