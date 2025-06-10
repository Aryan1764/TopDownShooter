using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after time to clean up
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject); // 💥 Destroy bullet on hit
        }

        // Optional: destroy bullet if it hits anything else (e.g., wall)
        // else if (!collision.isTrigger)
        // {
        //     Destroy(gameObject);
        // }
    }
}