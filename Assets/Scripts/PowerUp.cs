using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerType { Health, Speed, FireRate }
    public PowerType powerType;
    public int amount = 1;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (powerType == PowerType.Health)
        {
            var playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(amount);
            }
        }
        else if (powerType == PowerType.Speed)
        {
            var movement = collision.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.ApplySpeedBoost(amount, duration);
            }
        }
        else if (powerType == PowerType.FireRate)
        {
            var shooter = collision.GetComponent<PlayerShooting>();
            if (shooter != null)
            {
                shooter.ApplyFireRateBoost(amount, duration);
            }
        }

        Destroy(gameObject);
    }

}