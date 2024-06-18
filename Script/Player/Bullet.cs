using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Bullet : MonoBehaviour
{
    private int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the Enemy script
        BaseEnemy enemy = collision.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destroy the bullet on impact
        Destroy(gameObject);
    }
}

}
