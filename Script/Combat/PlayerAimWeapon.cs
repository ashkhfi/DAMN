using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    // private Animator playerAnimator;
    // private Animator gunAnimator;
    private Coroutine shootingCoroutine;

    public GameObject bulletPrefab; // Assign the bullet prefab in the Inspector
    public Transform firePoint; // Assign the fire point in the Inspector
    public float bulletSpeed = 20f;
    public float damage = 10f;
    public float fireRate = 0.1f; // Time between shots in seconds

    // Start is called before the first frame update
    void Start()
    {
        aimTransform = transform.Find("Aim");
        // playerAnimator = GetComponent<Animator>();

        // Assign the gun animator either via inspector or find the relevant child
        // gunAnimator = transform.Find("Aim/Gun").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
        
        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            StopCoroutine(shootingCoroutine);
        }
    }

    IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void Shoot()
    {
        // playerAnimator.SetTrigger("Shoot");
        // gunAnimator.SetTrigger("ShootTrigger");

        // Check if bulletPrefab is assigned
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.right * bulletSpeed;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(damage);
            }
        }
        else
        {
            Debug.LogError("Bullet prefab is not assigned.");
        }
    }
}

public static class UtilsClass
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
