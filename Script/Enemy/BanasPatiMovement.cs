using UnityEngine;

namespace Enemy
{
    public class BanasPatiMovement : BaseEnemy
    {
        public float speed = 2.0f;

        private Transform player;

        protected override void Start()
        {
            damage = 10; 
            health = 100;
            base.Start();

            // Mencari player berdasarkan tag
            player = GameObject.FindGameObjectWithTag("Player").transform;

            // Jika player tidak ditemukan, beri peringatan
            if (player == null)
            {
                Debug.LogWarning("Player not found!");
            }
        }

        void Update()
        {
            if (player != null)
            {
                MoveTowardsPlayer();
            }
        }

        void MoveTowardsPlayer()
        {
            // Menghitung arah menuju player
            Vector2 direction = (player.position - transform.position).normalized;

            // Menghitung posisi baru
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

            // Update posisi musuh
            transform.position = newPosition;
        }
    }
}
