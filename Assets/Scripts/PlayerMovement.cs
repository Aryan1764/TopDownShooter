using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float originalSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [HideInInspector]
    public Vector2 lastMoveDirection = Vector2.up;

    private Coroutine speedBoostRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = moveSpeed;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput.normalized;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    // ⚡ Called by power-up when picked up
    public void ApplySpeedBoost(int multiplier, float duration)
    {
        if (speedBoostRoutine != null)
            StopCoroutine(speedBoostRoutine);

        speedBoostRoutine = StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(int multiplier, float duration)
    {
        moveSpeed = originalSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }
}