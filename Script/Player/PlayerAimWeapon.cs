using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Animator gunAnimator;
    private Animator casingAnimator;
    private Animator muzzleFlashAnimator;
    private Coroutine shootingCoroutine;

    public GameObject bulletPrefab; // Assign the bullet prefab in the Inspector
    public Transform firePoint; // Assign the fire point in the Inspector
    public float bulletSpeed = 20f;
    public int damage = 10;
    public float fireRate = 0.1f; // Time between shots in seconds

    public AudioSource audioSource; // Assign the AudioSource component in the Inspector
    public AudioClip shootSound; // Assign the shooting sound effect in the Inspector

    void Start()
    {
        aimTransform = transform.Find("Aim");
        gunAnimator = aimTransform.Find("Gun").GetComponent<Animator>();
        casingAnimator = aimTransform.Find("Casing").GetComponent<Animator>();
        muzzleFlashAnimator = aimTransform.Find("MuzzleFlash").GetComponent<Animator>();

        // Check if the AudioSource component is assigned, if not, add it
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

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
        gunAnimator.SetTrigger("Shoot");
        casingAnimator.SetTrigger("Eject");
        muzzleFlashAnimator.SetTrigger("Flash");

        // Play the shooting sound
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

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
