using UnityEngine;

public class RajaIblisMovement : MonoBehaviour
{
    public float speed = 2.0f; // Movement speed of the enemy
    private float minX, maxX;

    private bool movingRight = true; // Indicates the direction of the enemy's movement
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Check if the Rigidbody2D component is present
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the GameObject.");
            return; // Exit Start method if Rigidbody2D is not found
        }

        rb.gravityScale = 1; // Set gravity to make the enemy fall

        // Get the camera bounds
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not found. Ensure the camera has the 'MainCamera' tag.");
            return; // Exit Start method if the main camera is not found
        }

        float camVertExtent = mainCamera.orthographicSize;
        float camHorzExtent = camVertExtent * Screen.width / Screen.height;
        minX = mainCamera.transform.position.x - camHorzExtent;
        maxX = mainCamera.transform.position.x + camHorzExtent;
    }

    void Update()
    {
        Move();
        ClampPosition();
    }

    void Move()
    {
        // Determine the direction of the enemy's movement
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void ClampPosition()
    {
        // If the enemy reaches the left or right bounds of the screen, change direction
        if (transform.position.x <= minX)
        {
            movingRight = true;
        }
        else if (transform.position.x >= maxX)
        {
            movingRight = false;
        }

        // Ensure the enemy stays within the screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}