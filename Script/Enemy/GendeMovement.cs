using UnityEngine;
using System.Collections;

namespace Enemy
{
    
public class GendeMovement : BaseEnemy
{
    public float moveSpeed = 2.0f; // Kecepatan gerakan musuh
    public float dashSpeed = 10.0f; // Kecepatan dash musuh
    public float dashCooldown = 5.0f; // Waktu antara dash
    public float dashDuration = 0.5f; // Durasi dash
    public float dashDistance = 5.0f; // Jarak dash

    private Transform playerTransform; // Referensi ke Transform pemain
    private Rigidbody2D rb; // Komponen Rigidbody2D musuh
    private float currentDashCooldown = 0f; // Timer cooldown untuk dash
    private bool isDashing = false; // Status apakah musuh sedang dash
    private Vector2 dashDirection; // Arah dash

    protected override void Start()
    {
        damage = 10; 
        health = 100;
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Cari pemain dan dapatkan Transform-nya
        rb = GetComponent<Rigidbody2D>(); // Dapatkan komponen Rigidbody2D
    }

    private void Update()
    {
        if (playerTransform == null)
            return; // Jika pemain tidak ditemukan, tidak melakukan apa-apa

        // Update timer cooldown untuk dash
        currentDashCooldown -= Time.deltaTime;

        // Jika sedang dash, lakukan pergerakan dash
        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;
            return;
        }

        // Gerakan musuh mengikuti pemain
        FollowPlayer();

        // Cek apakah musuh bisa melakukan dash (cooldown telah habis)
        if (CanDash())
        {
            StartCoroutine(Dash());
        }
    }

    private void FollowPlayer()
    {
        // Gerakan musuh menuju ke arah pemain dengan kecepatan yang ditentukan
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private bool CanDash()
    {
        // Cek apakah cooldown untuk dash telah habis
        return currentDashCooldown <= 0f;
    }

    private IEnumerator Dash()
    {
        isDashing = true; // Set status sedang dash
        dashDirection = (playerTransform.position - transform.position).normalized; // Tentukan arah dash

        // Set velocity untuk dash
        rb.velocity = dashDirection * dashSpeed;

        // Tunggu selama durasi dash
        yield return new WaitForSeconds(dashDuration);

        // Reset velocity setelah dash selesai
        rb.velocity = Vector2.zero;

        // Reset timer cooldown untuk dash
        currentDashCooldown = dashCooldown;

        isDashing = false; // Set status tidak sedang dash
    }

   
}

}