using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public Slider healthSlider; // UI Slider
    public GameObject gameOverScreen; // Game Over UI

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("PLAYER DEAD!");
        Time.timeScale = 0f;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // In case game was paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

}