// ExperienceOrb.cs
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [Header("Chasing Settings")]
    public bool isChasing = false;
    public float pullStrength = 2.0f; // Adjust this value based on your preference

    public int experienceValue = 10;

    public float accelerationRate = 1;

    private GameObject player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            ApplyRandomForce();
        }
        else
        {
            Debug.LogWarning("Rigidbody2D not found on the item. Make sure to add a Rigidbody2D component to enable physics.");
        }
    }

    private void Update()
    {
        if (isChasing && player != null)
        {
            // Pull towards the player
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

            // Calculate additional force if the orb is deviating from a straight path towards the player
            float deviationFactor = 0.2f; // Adjust this value based on your preference
            Vector2 deviationForce = Vector2.zero;

            // Check if the orb is deviating in the x-direction
            if (Mathf.Abs(Vector2.Dot(rb.velocity.normalized, directionToPlayer)) < 0.9f)
            {
                deviationForce.x = Mathf.Sign(directionToPlayer.x - transform.position.x) * deviationFactor;
            }

            // Check if the orb is deviating in the y-direction
            if (Mathf.Abs(Vector2.Dot(rb.velocity.normalized, directionToPlayer)) < 0.9f)
            {
                deviationForce.y = Mathf.Sign(directionToPlayer.y - transform.position.y) * deviationFactor;
            }

            // Apply a force to pull the orb towards the player
            accelerationRate += accelerationRate * Time.deltaTime;
            rb.velocity = (directionToPlayer + deviationForce) * pullStrength * accelerationRate;
        }
    }

    private void ApplyRandomForce()
    {
        // Generate random values for x and y components of the force
        float randomX = Random.Range(-100f, 100f);
        float randomY = Random.Range(-100f, 100f);

        // Create a random force vector
        Vector2 randomForce = new Vector2(randomX, randomY);

        // Apply the random force to the item
        rb.AddForce(randomForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Give experience to the player
            GameController.instance.GrantXp(experienceValue);

            // Destroy the experience orb
            Destroy(gameObject);
        }
    }
}
