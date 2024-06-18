using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int kills;
    private int targetKills; // Jumlah kill yang dibutuhkan untuk menang
    public Text killsText; // Referensi ke UI Text untuk menampilkan jumlah kill
    public Text countdownText; // Referensi ke UI Text untuk menampilkan countdown waktu

    private float initialTime = 4f; // Waktu awal countdown (dalam detik)
    private float timeRemaining; // Waktu tersisa (dalam detik)
    private bool countdownActive = true; // Apakah countdown aktif
    private EnemySpawner enemySpawner;
    private bool isGameEnded = false; // Apakah permainan telah berakhir

    private void Awake()
    {
        float cek = PlayerPrefs.GetFloat("TimeRemaining", 0f);
        float cek1 = PlayerPrefs.GetInt("TotalKills", 0);
        float cek2 = PlayerPrefs.GetInt("TotalEnemy", 0);
        Debug.Log("Time Remaining: " + cek);
        Debug.Log("Kill : " + cek1);
        Debug.Log("total enemy : " + cek2);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        // Atur waktu awal countdown
        timeRemaining = initialTime;
        enemySpawner = FindObjectOfType<EnemySpawner>(); 
        if (enemySpawner != null)
        {
            // Dapatkan totalPossibleEnemies dari EnemySpawner
            int totalPossibleEnemies = enemySpawner.GetTotalPossibleEnemies();
            targetKills = totalPossibleEnemies;
        }
        else
        {
            Debug.LogError("EnemySpawner tidak ditemukan di scene!");
        }

        // Memulai coroutine untuk countdown waktu
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        // Cek apakah permainan telah berakhir
        if (!isGameEnded)
        {
            // Update teks jumlah kill
            UpdateKillsText();

            // Cek apakah pemain menang (jumlah kill mencapai target)
            if (kills >= targetKills)
            {
                EndGame(true);
            }
        }
    }

    public void AddKill()
    {
        kills++;
    }

    private void UpdateKillsText()
    {
        if (killsText != null)
        {
            killsText.text = kills.ToString();
        }
    }

    // Fungsi untuk mengatur waktu awal countdown
    public void SetInitialTime(float time)
    {
        initialTime = time;
    }

    private IEnumerator Countdown()
    {
        // Countdown waktu selama waktu awal yang ditetapkan
        while (timeRemaining > 0 && countdownActive && !isGameEnded)
        {
            // Kurangi waktu tersisa
            timeRemaining -= Time.deltaTime;

            // Konversi waktu tersisa menjadi format menit:detik
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Update teks countdown
            if (countdownText != null)
            {
                countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            // Tunggu satu frame
            yield return null;
        }

        // Jika countdown selesai, atau permainan belum berakhir tapi waktu habis, maka permainan berakhir karena kalah
        if (!isGameEnded)
        {
            EndGame(false);
        }
    }

    // Fungsi untuk menangani akhir permainan
    public void EndGame(bool win)
    {
        isGameEnded = true;

        if (win)
        {
            Debug.Log("You Win!");

            // Simpan data ke PlayerPrefs jika pemain menang
            PlayerPrefs.SetInt("TotalKills", kills);
            PlayerPrefs.SetInt("TotalEnemy", targetKills);
            PlayerPrefs.SetFloat("TimeRemaining", Mathf.Max(timeRemaining, 0)); // Pastikan waktu tersisa tidak negatif
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Game Over!");
            PlayerPrefs.SetInt("TotalKills", kills);
            PlayerPrefs.SetInt("TotalEnemy", targetKills);
            PlayerPrefs.SetFloat("TimeRemaining", Mathf.Max(timeRemaining, 0)); // Pastikan waktu tersisa tidak negatif
            PlayerPrefs.Save();
        }

    }
}
