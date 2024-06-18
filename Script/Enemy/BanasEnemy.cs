using UnityEngine;

public class BanasEnemy : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballCooldown = 2f;
    public float moveCooldown = 5f; // Waktu antara perpindahan tempat
    public float moveDuration = 2f; // Durasi pergerakan musuh

    private Transform playerTransform; // Referensi ke Transform pemain
    private float currentFireballCooldown = 0f; // Timer cooldown untuk menembak
    private float currentMoveCooldown = 0f; // Timer cooldown untuk berpindah tempat
    private bool isMoving = false; // Status apakah musuh sedang bergerak
    private Vector3 targetPosition; // Posisi target saat berpindah tempat

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Cari pemain dan dapatkan Transform-nya
        currentMoveCooldown = moveCooldown; // Inisialisasi timer cooldown untuk berpindah tempat
        SetNewTargetPosition(); // Tentukan posisi target awal
    }

    private void Update()
    {
        if (playerTransform == null)
            return; // Jika pemain tidak ditemukan, tidak melakukan apa-apa

        // Update timer cooldown untuk menembak
        currentFireballCooldown -= Time.deltaTime;

        // Cek apakah musuh bisa menembak (cooldown telah habis)
        if (CanShoot())
        {
            ShootFireball();
            currentFireballCooldown = fireballCooldown; // Reset timer cooldown untuk menembak
        }

        // Update timer cooldown untuk berpindah tempat
        currentMoveCooldown -= Time.deltaTime;

        // Jika musuh sedang bergerak, lakukan perpindahan
        if (isMoving)
        {
            MoveToTarget();
        }
        else if (currentMoveCooldown <= 0f)
        {
            // Jika cooldown untuk berpindah tempat telah habis dan musuh tidak sedang bergerak, mulai perpindahan
            isMoving = true;
            currentMoveCooldown = moveCooldown; // Reset timer cooldown untuk berpindah tempat
            SetNewTargetPosition(); // Tentukan posisi target baru
        }
    }

    private bool CanShoot()
    {
        // Cek apakah cooldown telah habis
        return currentFireballCooldown <= 0f;
    }

    private void ShootFireball()
    {
        // Instantiate sebuah fireball prefab di posisi dan rotasi fire point
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.target = playerTransform; // Set target fireball ke Transform pemain
        }
    }

    private void SetNewTargetPosition()
    {
        // Tentukan posisi target baru secara acak dalam batas layar
        float minX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float minY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float maxY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);
    }

    private void MoveToTarget()
    {
        // Lakukan pergerakan musuh ke posisi target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveDuration * Time.deltaTime);

        // Jika sudah mencapai posisi target, hentikan pergerakan
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
}
