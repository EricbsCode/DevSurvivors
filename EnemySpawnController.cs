using System.Collections.Generic;
using UnityEngine;

// Class representing an enemy type with its cost
[System.Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
    public int cost;
}

public class EnemySpawnController : MonoBehaviour
{
    public int increaseRate = 1;

    public int currentBudget;
    public static int currentEnemyCount;
    public List<EnemyType> enemyTypes; // List of different enemy types with costs
    public float spawnRadius = 10f; // Radius around the player to spawn enemies

    void Start()
    {
        currentEnemyCount = 0;
        // Repeatedly spawn enemies every second
        currentBudget = 0;
        InvokeRepeating("IncreaseBudget", 1.0f, 1.0f); // Increase budget every second
        InvokeRepeating("BuySpawns", 1.0f, 1.0f);
    }

    private void IncreaseBudget()
    {
        currentBudget += increaseRate;
        increaseRate++;
    }

    void BuySpawns()
    {
        // Customize this logic based on your budget management
        // For now, it buys as many enemies as possible with the current budget

        foreach (var enemyType in enemyTypes)
        {
            if (currentEnemyCount <= 100)
            {
                // Calculate the number of enemies that can be bought with the current budget and enemy cost
                int numEnemiesToBuy = currentBudget / enemyType.cost;

                // Buy the enemies
                SpawnEnemies(enemyType, numEnemiesToBuy);

                // Decrease the budget based on the cost of the bought enemies
                currentBudget -= numEnemiesToBuy * enemyType.cost;
            }
        }
    }

    void SpawnEnemies(EnemyType enemyType, int numEnemiesToSpawn)
    {
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            // Calculate a random angle within a circle
            float angle = Random.Range(0f, 360f);
            // Convert the angle to radians
            float angleRad = angle * Mathf.Deg2Rad;

            // Calculate a random position within the spawn radius
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * spawnRadius;

            // Spawn the enemy prefab at the calculated position
            Instantiate(enemyType.enemyPrefab, spawnPosition, Quaternion.identity);
            currentEnemyCount++;
        }
    }
}
