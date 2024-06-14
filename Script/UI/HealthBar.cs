using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Referensi ke komponen Slider
    public int maxHealth = 100; // Kesehatan maksimum
    private int currentHealth; // Kesehatan saat ini

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }


    // Fungsi untuk mengurangi kesehatan
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();
    }

    // Fungsi untuk menambah kesehatan
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    // Fungsi untuk memperbarui tampilan health bar
    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }
}
