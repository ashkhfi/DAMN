using UnityEngine;

public class KuntiAnak : MonoBehaviour
{
    public float speed = 2.0f;
    public float changeDirectionTime = 2.0f;

    private Vector2 direction;
    private float timer;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        SetRandomDirection();

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
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            SetRandomDirection();
            timer = 0f;
        }

        Move();
    }

    void SetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    void Move()
    {
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
