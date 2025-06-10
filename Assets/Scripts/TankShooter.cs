using UnityEngine;

public class TankShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;
    public float bulletSpeed = 5f;

    private Transform player;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }
}