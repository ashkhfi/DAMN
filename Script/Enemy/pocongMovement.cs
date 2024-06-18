using UnityEngine;

public class pocongMovement : BaseEnemy
{
public float horizontalSpeed = 2.0f; // Kecepatan gerakan horizontal
public float verticalSpeed = 5.0f; // Kecepatan lompatan
public float jumpInterval = 2.0f; // Interval antara loncatan
public bool isGrounded; // Menunjukkan apakah Pocong menyentuh tanah


private Rigidbody2D rb;
private float nextJumpTime = 0f;
private float minX, maxX, minY, maxY;

protected override void Start()
{
    damage = 10; 
    health = 100;
    base.Start();
    rb = GetComponent<Rigidbody2D>();

    // Mendapatkan batas-batas kamera
    Camera mainCamera = Camera.main;
    float camVertExtent = mainCamera.orthographicSize;
    float camHorzExtent = camVertExtent * Screen.width / Screen.height;
    minX = mainCamera.transform.position.x - camHorzExtent;
    maxX = mainCamera.transform.position.x + camHorzExtent;
    minY = mainCamera.transform.position.y - camVertExtent;
    maxY = mainCamera.transform.position.y + camVertExtent;
}

void Update()
{
    // Cek apakah Pocong menyentuh tanah
    isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

    // Gerakan horizontal
    float horizontalMovement = horizontalSpeed * Time.deltaTime;
    transform.Translate(new Vector2(horizontalMovement, 0));

    // Lompat-lompat
    if (Time.time > nextJumpTime && isGrounded)
    {
        Jump();
        nextJumpTime = Time.time + jumpInterval;
    }

    // Mencegah Pocong keluar dari batas kamera
    ClampPosition();
}

void Jump()
{
    rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
}

void ClampPosition()
{
    Vector3 clampedPosition = transform.position;
    clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
    clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
    transform.position = clampedPosition;

    // Jika Pocong berada di batas kamera, balik arah gerakannya
    if (transform.position.x <= minX || transform.position.x >= maxX)
    {
        horizontalSpeed *= -1;
    }
}
}