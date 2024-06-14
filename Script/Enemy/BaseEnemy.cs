using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int health;
    public int damage;

    protected virtual void Start()
    {
        // Pastikan musuh memiliki tag "Enemy"
        gameObject.tag = "Enemy";
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle the death of the target
        Destroy(gameObject);
    }
}
