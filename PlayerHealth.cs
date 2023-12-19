using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar; // Reference to the HealthBar script

    private void Start()
    {
        currentHealth = maxHealth;
        // Ensure the healthBar reference is set in the Inspector
        if (healthBar == null)
        {
            Debug.LogError("PlayerHealth is missing a reference to the healthBar.");
        }
        else
        {
            // Set initial values on the health bar
            healthBar.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        // Apply damage to the player
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        healthBar.UpdateHealth(currentHealth, maxHealth);

        // Check if the player is defeated
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        // Implement any logic for player death here
        Debug.Log("Player has been defeated!");
    }

    // Function for healing (if needed)
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }
}
