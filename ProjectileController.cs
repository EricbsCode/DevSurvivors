using UnityEngine;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour
{
    // Damage value specific to each weapon
    public int weaponDamage = 1;

    // Maximum number of hits for the projectile
    public int maxHits = 1;

    // Lifespan of the projectile in seconds
    public float lifespan = 5f;

    private HashSet<Collider2D> hitTargets = new HashSet<Collider2D>();

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && hitTargets.Count < maxHits)
        {
            // Check if this target has already been hit
            if (!hitTargets.Contains(other))
            {
                // Calculate the damage based on the weapon
                int calculatedDamage = CalculateDamage();

                // Apply the damage to the enemy
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(calculatedDamage);
                }

                // Add the target to the hit list
                hitTargets.Add(other);

                // Check if no remaining hits, then destroy the projectile
                if (hitTargets.Count >= maxHits)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Function to calculate damage based on weapon attributes
    private int CalculateDamage()
    {
        // Implement your logic here to calculate damage based on weapon attributes
        // For example, you can consider player stats, upgrades, etc.
        return weaponDamage;
    }
}
