using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;              // Bullet to spawn
    public Transform bulletSpawnPoint;           // Where to spawn the bullet from

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Shoot();

            if (anim != null)
            {
                anim.SetTrigger("Shoot");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (anim != null)
            {
                anim.SetTrigger("Idle");
            }
        }
    }

    void Shoot()
    {
        // Convert mouse position to world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from bullet spawn to mouse pointer
        Vector2 shootDirection = (mouseWorldPos - bulletSpawnPoint.position).normalized;

        // Spawn the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Pass the direction to the bullet
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(shootDirection);
        }
    }
}