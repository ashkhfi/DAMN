using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float[] spawnTimes; // Waktu spawn untuk setiap tipe musuh
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            float spawnTime = spawnTimes[i];
            InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
        }
    }

    void SpawnEnemy()
    {
        // Pilih prefab musuh secara acak
        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Mendapatkan posisi layar di sisi kanan
        Vector3 screenRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), mainCamera.nearClipPlane));
        // Tambahkan offset agar musuh muncul di luar layar
        Vector3 spawnPosition = new Vector3(screenRight.x, screenRight.y, 0);
        Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
    }
}
