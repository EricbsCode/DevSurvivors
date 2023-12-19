using UnityEngine;

public class ItemExpVacuum : MonoBehaviour
{
    private CircleCollider2D pickupCollider;
    public PlayerController playerController;

    private void Start()
    {
        // Get the CircleCollider2D component attached to this GameObject
        pickupCollider = GetComponent<CircleCollider2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on the GameObject.");
        }
        if (pickupCollider == null)
        {
            Debug.LogError("CircleCollider2D not found on the GameObject. Make sure to attach the script to a GameObject with a CircleCollider2D.");
        }
    }

    // Function to increase the radius by a percentage
    public void LevelExpVacuum(int TimesChosen)
    {
        if (TimesChosen <= 4)
        {
            ApplyExpVacuumModifier(.1f);
        }
        else if (TimesChosen == 5)
        {
            ApplyExpVacuumModifier(.2f);
            playerController.UpdateSpeedModifier(.2f);
        }
        else
        {
            Debug.LogError("Error: Attempting to Level Exp Vacuum Past Maximum.");
        }
    }

    public void ApplyExpVacuumModifier(float Modifier)
    {
        if (pickupCollider != null)
        {
            // Calculate the increase amount based on the current radius
            float increaseAmount = pickupCollider.radius * (Modifier);

            // Increase the radius
            pickupCollider.radius += increaseAmount;
        }
    }
}
