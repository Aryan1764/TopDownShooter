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

   
    private float damageCooldown = 1f;
    private float lastDamageTime;

   
    public event System.Action OnDeath;

    void Start()
    {
        currentHealth = maxHealth;

      
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        
        healthSlider = GetComponentInChildren<Slider>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

      
        healthBarTransform = healthSlider.transform.parent;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }

        
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
            OnDeath?.Invoke(); 
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            TryDamagePlayer(collision.collider);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            TryDamagePlayer(collision.collider);
        }
    }

    private void TryDamagePlayer(Collider2D playerCollider)
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            PlayerHealth ph = playerCollider.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(1);
                lastDamageTime = Time.time;
            }
        }
    }
}
