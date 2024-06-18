using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EnemyNamespace
{
    [System.Serializable]
    public class Enemys
    {
        public GameObject prefab;
        public int totalMusuh; // Jumlah total musuh yang akan muncul untuk tipe ini
    }
}

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyNamespace.Enemys> enemies; // Daftar musuh
    private Camera mainCamera;
    private Dictionary<EnemyNamespace.Enemys, int> enemyCounts; // Menyimpan jumlah musuh yang sudah muncul
    private Dictionary<EnemyNamespace.Enemys, List<GameObject>> activeEnemies; // Menyimpan referensi musuh yang aktif

    private int totalPossibleEnemies; // Menyimpan hasil penjumlahan totalMusuh dari setiap musuh dalam daftar

    void Start()
    {
        mainCamera = Camera.main;
        enemyCounts = new Dictionary<EnemyNamespace.Enemys, int>();
        activeEnemies = new Dictionary<EnemyNamespace.Enemys, List<GameObject>>();

        // Hitung totalPossibleEnemies
        totalPossibleEnemies = 0;
        foreach (var enemy in enemies)
        {
            enemyCounts[enemy] = 0; // Inisialisasi jumlah musuh yang muncul untuk setiap tipe musuh
            activeEnemies[enemy] = new List<GameObject>(); // Inisialisasi daftar musuh yang aktif untuk setiap tipe musuh
            totalPossibleEnemies += enemy.totalMusuh; // Tambahkan totalMusuh dari setiap musuh ke totalPossibleEnemies
            // Memulai spawning untuk setiap musuh
            StartCoroutine(ManageEnemySpawning(enemy));
        }
    }

    private IEnumerator ManageEnemySpawning(EnemyNamespace.Enemys enemy)
    {
        // Spawn dua musuh di awal
        for (int i = 0; i < 2; i++)
        {
            if (enemyCounts[enemy] < enemy.totalMusuh)
            {
                SpawnEnemy(enemy);
            }
        }

        while (enemyCounts[enemy] < enemy.totalMusuh)
        {
            // Jika jumlah musuh yang aktif kurang dari 2, spawn musuh baru
            while (activeEnemies[enemy].Count < 2 && enemyCounts[enemy] < enemy.totalMusuh)
            {
                SpawnEnemy(enemy);
                yield return null; // Menunggu satu frame untuk memastikan spawning tidak terlalu cepat
            }
            yield return new WaitForSeconds(1f); // Cek lagi setiap detik
        }
    }

    private void SpawnEnemy(EnemyNamespace.Enemys enemy)
    {
        if (enemy.prefab == null)
        {
            Debug.LogError("Prefab untuk musuh belum diatur!");
            return;
        }

        // Mendapatkan posisi layar di sisi kanan
        Vector3 screenRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), mainCamera.nearClipPlane));
        // Tambahkan offset agar musuh muncul di luar layar
        Vector3 spawnPosition = new Vector3(screenRight.x, screenRight.y, 0);
        GameObject newEnemy = Instantiate(enemy.prefab, spawnPosition, Quaternion.identity);
        activeEnemies[enemy].Add(newEnemy);
        enemyCounts[enemy]++;

        // Tambahkan event listener untuk mendeteksi kematian musuh
        BaseEnemy baseEnemy = newEnemy.GetComponent<BaseEnemy>();
        if (baseEnemy != null)
        {
            baseEnemy.OnDeath += (GameObject deadEnemy) => OnEnemyDeath(enemy, deadEnemy);
        }
        else
        {
            Debug.LogError("Prefab musuh tidak memiliki komponen BaseEnemy!");
        }
    }

    private void OnEnemyDeath(EnemyNamespace.Enemys enemy, GameObject deadEnemy)
    {
        activeEnemies[enemy].Remove(deadEnemy);
    }

    public int GetTotalPossibleEnemies()
    {
        return totalPossibleEnemies;
    }
}
