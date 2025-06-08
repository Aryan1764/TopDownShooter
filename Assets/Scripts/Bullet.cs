using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    private Vector2 moveDirection = Vector2.up;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var drone = collision.GetComponent<EnemyDrone>();
            if (drone != null)
            {
                drone.TakeDamage(1);
            }

            Destroy(gameObject); // Bullet disappears on hit
        }
    }
}