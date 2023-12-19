using UnityEngine;

public class ExperienceDrop : MonoBehaviour
{
    private int experienceValue;

    // Set the experience value for this drop
    public void SetExperienceValue(int value)
    {
        experienceValue = value;
    }

    // You can add more logic here, such as animating the drop or triggering other effects
    private void Start()
    {
        // Example: Destroy the experience drop after a certain time (adjust as needed)
        Destroy(gameObject, 5f);
    }
}
