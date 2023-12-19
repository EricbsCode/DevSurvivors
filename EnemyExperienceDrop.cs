using UnityEngine;

public class EnemyExperienceDrop : MonoBehaviour
{
    public GameObject experienceDropPrefab;

    // Adjust this value based on the experience you want each enemy to drop
    public int experienceValue = 10;

    private void Start()
    {
        // Ensure that the experienceDropPrefab is assigned in the inspector
        if (experienceDropPrefab == null)
        {
            Debug.LogError("Experience Drop Prefab is not assigned!");
        }
    }

    // Call this method when the enemy dies
    public void SpawnExperienceOrb()
    {
        // Instantiate the experience drop prefab at the enemy's position
        GameObject experienceDrop = Instantiate(experienceDropPrefab, transform.position, Quaternion.identity);

        // Pass the experience value to the experience drop prefab (you can modify your prefab script to handle this)
        experienceDrop.GetComponent<ExperienceDrop>().SetExperienceValue(experienceValue);

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
