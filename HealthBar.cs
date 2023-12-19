using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public static Image healthBarImage;
    public static TextMeshProUGUI healthText;

    private void Start()
    {
        // Ensure the healthBarImage and healthText references are set in the Inspector
        if (healthBarImage == null || healthText == null)
        {
            Debug.LogError("HealthBar is missing references to healthBarImage or healthText.");
            return;
        }

        // Set initial values
        UpdateHealth(100, 100); // Assuming max health is 100 at the start
    }

    // Function to update the health bar and text
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        // Ensure the maxHealth is not zero to avoid division by zero
        if (maxHealth <= 0)
        {
            Debug.LogError("Max health should be greater than zero.");
            return;
        }

        // Calculate the health percentage
        float healthPercentage = (float)currentHealth / maxHealth;

        // Update the health bar image fill amount
        healthBarImage.fillAmount = healthPercentage;

        // Update the health text
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
}
