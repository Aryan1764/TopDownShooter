using UnityEngine;
using UnityEngine.UI;

public class EnemyDrone : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 3;
    private int currentHealth;

    private Transform player;
    private Slider healthSlider;
    private Transform healthBarTransform;

    void Start()
    {
        currentHealth = maxHealth;

        // Get reference to the player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        // Get reference to the Slider (assumes only one Slider exists in children)
        healthSlider = GetComponentInChildren<Slider>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        // Optional: Cache the health bar's transform if you want to offset its position
        healthBarTransform = healthSlider.transform.parent; // assumes HealthBar is parent of Slider
    }

    void Update()
    {
        // Chase the player
        if (player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }

        // Keep the health bar above the enemy (optional if it's static in local position)
        if (healthBarTransform != null)
        {
            healthBarTransform.position = transform.position + Vector3.up * 1.5f;
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
            Destroy(gameObject); // This also removes the UI since it's a child
        }
    }
}