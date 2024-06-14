using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EnemyNamespace
{
    [System.Serializable]
    public class Enemy
    {
        public GameObject prefab;
        public float spawnStartTime;
        public float spawnInterval;
    }
}

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyNamespace.Enemy> enemies; // Daftar musuh
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        foreach (var enemy in enemies)
        {
            // Memulai spawning untuk setiap musuh berdasarkan waktu mulai dan intervalnya
            StartCoroutine(SpawnEnemy(enemy));
        }
    }

    private IEnumerator SpawnEnemy(EnemyNamespace.Enemy enemy)
    {
        // Tunggu hingga waktu mulai spawn
        yield return new WaitForSeconds(enemy.spawnStartTime);

        while (true)
        {
            // Mendapatkan posisi layar di sisi kanan
            Vector3 screenRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), mainCamera.nearClipPlane));
            // Tambahkan offset agar musuh muncul di luar layar
            Vector3 spawnPosition = new Vector3(screenRight.x, screenRight.y, 0);
            Instantiate(enemy.prefab, spawnPosition, Quaternion.identity);

            // Tunggu hingga waktu interval berikutnya
            yield return new WaitForSeconds(enemy.spawnInterval);
        }
    }
}
