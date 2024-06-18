using UnityEngine;

public class TanahHorizontal : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Kecepatan gerakan tile
    public float moveDistance = 5.0f; // Jarak total yang ditempuh tile ke kiri dan kanan

    private Vector3 startPosition; // Posisi awal tile
    private bool movingRight = true; // Arah gerakan tile

    void Start()
    {
        startPosition = transform.position; // Simpan posisi awal tile
    }

    void Update()
    {
        // Hitung jarak yang telah ditempuh dari posisi awal
        float distance = Vector3.Distance(startPosition, transform.position);

        // Jika tile telah menempuh jarak maksimum, balik arah gerakan
        if (distance >= moveDistance)
        {
            movingRight = !movingRight;
            startPosition = transform.position; // Reset posisi awal
        }

        // Gerakkan tile ke arah yang sesuai
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") | collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") | collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.SetParent(null);
        }
    }
}
