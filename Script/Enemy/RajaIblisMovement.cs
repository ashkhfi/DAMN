using UnityEngine;
namespace Enemy
{
public class RajaIblisMovement : BaseEnemy
{
    public float speed = 2.0f; // Kecepatan gerakan musuh
    private float minX, maxX;

    private bool movingRight = true; // Menunjukkan arah gerakan musuh

    protected override void Start()
    {
        // Mendapatkan batas-batas kamera
        Camera mainCamera = Camera.main;
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
        // Menentukan arah gerakan musuh
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void ClampPosition()
    {
        // Jika musuh mencapai batas kiri atau kanan layar, balik arah gerakan
        if (transform.position.x <= minX)
        {
            movingRight = true;
        }
        else if (transform.position.x >= maxX)
        {
            movingRight = false;
        }

        // Pastikan musuh tetap berada dalam batas layar
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}

}
