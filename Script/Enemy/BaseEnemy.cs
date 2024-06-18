using UnityEngine;

namespace Enemy
{
    
    public class BaseEnemy : MonoBehaviour
    {
        public int health;
        public int damage;

        public delegate void DeathEventHandler(GameObject enemy);
        public event DeathEventHandler OnDeath;

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
            // Tambahkan jumlah kill saat musuh mati
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddKill();
            }

            // Panggil event OnDeath sebelum menghancurkan objek
            if (OnDeath != null)
            {
                OnDeath(gameObject);
            }

            // Hancurkan objek musuh
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Jika bertabrakan dengan pemain, berikan damage
            if (other.CompareTag("Player"))
            {
                // Mengambil komponen Health dari pemain dan mengurangi health-nya
                HealthBar playerHealth = other.GetComponent<HealthBar>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }

}