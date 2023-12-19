// ExperiencePickUpRangeController.cs
using UnityEngine;

public class ExperiencePickUpRangeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExperienceOrb"))
        {

            // Get the ExperienceOrb component from the collided object
            ExperienceOrb experienceOrb = other.GetComponent<ExperienceOrb>();
            Debug.Log("Collided with orb!");

            if (experienceOrb != null)
            {
                // Set chasing to true
                experienceOrb.isChasing = true;
            }
        }
    }
}
