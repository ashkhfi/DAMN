using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the Enemy script
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destroy the bullet on impact
        Destroy(gameObject);
    }
}
