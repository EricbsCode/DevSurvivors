// CursorProjectileController.cs
using UnityEngine;

public class CursorProjectileController : BaseWeaponController
{
    public GameObject projectilePrefab;
    public float DefaultProjectileSpeed = 30f;

    [Header("Debug")]
    public GameObject nearestEnemy;

    protected override void PerformAction()
    {
        // Instantiate the projectile prefab
        if (FindNearestEnemy() == null)
            {
                return;
            }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Customize projectile behavior
        CustomizeProjectile(projectile);
    }

    private void CustomizeProjectile(GameObject projectile)
    {
        // Find the nearest enemy
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            // Calculate the direction from the projectile to the nearest enemy
            Vector3 directionToEnemy = nearestEnemy.transform.position - projectile.transform.position;
            
            // Normalize the direction to get a unit vector
            Vector3 normalizedDirection = directionToEnemy.normalized;

            // Check if the instantiated projectile has a Rigidbody2D
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Set the projectile's speed and direction towards the nearest enemy
                rb.velocity = normalizedDirection * DefaultProjectileSpeed;

                // Calculate the rotation angle in degrees
                float angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;

                // Since your default cursor direction is upwards, adjust the angle accordingly
                angle -= 90f; // Subtract 90 degrees to align with the upward direction

                // Rotate the projectile around the Y-axis
                projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }



    private GameObject FindNearestEnemy()
    {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    if (enemies.Length == 0)
    {
        // No enemies found
        return null;
    }

    GameObject nearestEnemy = null;
    float shortestDistance = Mathf.Infinity;
    Vector3 currentPosition = transform.position;

    foreach (GameObject enemy in enemies)
    {
        float distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);

        if (distanceToEnemy < shortestDistance)
        {
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }
    }

    return nearestEnemy;
    }

}
