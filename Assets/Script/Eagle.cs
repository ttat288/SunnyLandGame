using System.Collections;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float diveDuration = 1f;
    [SerializeField] private float hurtForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerController playerController;

    private Collider2D coll;
    private new Rigidbody2D rb; // Use the 'new' keyword to hide the inherited 'rb' member
    private Vector3 originalPosition;
    private bool isDiving = false;
    private bool hitPlayer = false;

    private enum State { idle, diving, hurt, returning }
    private State state = State.idle;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (state == State.idle)
        {
            float playerPosition = playerController.PlayerLocaltion();
            if (!isDiving && playerPosition >= leftCap && playerPosition <= rightCap)
            {
                StartCoroutine(DelayedDive());
            }
        }
        else if (state == State.hurt)
        {
            // Additional logic can be added here if needed during the hurt state
        }
    }

    private IEnumerator DelayedDive()
    {
        isDiving = true;
        state = State.diving;
        yield return new WaitForSeconds(2f); // Delay before diving
        StartCoroutine(DiveTowardsPlayer());
    }

    private IEnumerator DiveTowardsPlayer()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = playerController.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < diveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / diveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isDiving = false;

        if (hitPlayer)
        {
            state = State.hurt;
            if (playerController.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
            yield return new WaitForSeconds(1f); // Wait for 1 second after knockback
            StartCoroutine(ReturnToOriginalPosition());
            hitPlayer = false; // Reset flag
        }
        else
        {
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        state = State.returning;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = originalPosition;
        float returnDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < returnDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        state = State.idle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitPlayer = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere to visualize the player hit detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f); // Adjust the radius as needed
    }

    private void Attack()
    {
        // Add attack logic if necessary
    }
}
