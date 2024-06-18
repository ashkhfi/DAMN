using System.Collections;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float flySpeed = 2f;
    public float dashSpeed = 10f;
    public float detectionRange = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float dashDeviation = 0.5f; // Variasi arah dash
    public LayerMask playerLayer;

    private Transform player;
    private bool isDashing = false;
    private bool isCooldown = false;
    private Vector2 dashDirection;
    private Vector2 flyDirection;
    private Camera mainCamera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        StartCoroutine(ChangeFlyDirection());
    }

    void Update()
    {
        if (!isDashing)
        {
            FlyAround();
            if (!isCooldown)
            {
                DetectPlayer();
            }
        }
        ConstrainToCamera();
    }

    void FlyAround()
    {
        transform.Translate(flyDirection * flySpeed * Time.deltaTime);
    }

    void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (playerCollider != null)
        {
            isDashing = true;
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            dashDirection = directionToPlayer + new Vector2(Random.Range(-dashDeviation, dashDeviation), Random.Range(-dashDeviation, dashDeviation));
            dashDirection.Normalize(); // Normalisasi vektor untuk memastikan panjangnya 1
            StartCoroutine(DashTowardsPlayer());
        }
    }

    IEnumerator DashTowardsPlayer()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isCooldown = false;
    }

    IEnumerator ChangeFlyDirection()
    {
        while (true)
        {
            flyDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(Random.Range(1f, 3f)); // Change direction every 1 to 3 seconds
        }
    }

    void ConstrainToCamera()
    {
        Vector3 pos = transform.position;
        Vector3 min = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 max = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
