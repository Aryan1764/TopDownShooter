using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;              
    public Transform bulletSpawnPoint;           

    public float fireRate = 0.5f;             
    private float fireCooldown = 0f;
    private float originalFireRate;
    private Coroutine fireRateBoostRoutine;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        originalFireRate = fireRate;
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;

            if (anim != null)
            {
                anim.SetTrigger("Shoot");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (anim != null)
            {
                anim.SetTrigger("Idle");
            }
        }
    }

    void Shoot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mouseWorldPos - bulletSpawnPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(shootDirection);
            
        }
        if (AudioManager.Instance != null && AudioManager.Instance.shootClip != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.shootClip);
        }

    }

  
    public void ApplyFireRateBoost(float multiplier, float duration)
    {
        if (fireRateBoostRoutine != null)
            StopCoroutine(fireRateBoostRoutine);

        fireRateBoostRoutine = StartCoroutine(FireRateBoostRoutine(multiplier, duration));
    }

    private System.Collections.IEnumerator FireRateBoostRoutine(float multiplier, float duration)
    {
        fireRate = originalFireRate / multiplier;
        yield return new WaitForSeconds(duration);
        fireRate = originalFireRate;
    }
}
