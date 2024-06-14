using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timerText; // Referensi ke komponen Text
    private float elapsedTime; // Waktu yang sudah berlalu

    void Start()
    {
        elapsedTime = 0f;
    }

    void Update()
    {
        // Perbarui waktu yang sudah berlalu
        elapsedTime += Time.deltaTime;
        
        // Format waktu menjadi menit dan detik
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        
        // Set text timer
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
