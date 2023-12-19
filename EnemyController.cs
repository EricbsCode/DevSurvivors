using System.Collections;
using UnityEngine;

public class EnemyController : Mover
{
    private Transform playerTransform;
    private Vector3 startingPosition;
    private BoxCollider2D hitbox;
    public GameObject experienceDropPrefab;
    public PlayerController playerController;
    private GameObject player;

    // Additional variable to store enemy health
    public int maxHealth = 10;
    private int currentHealth;

    // Default movement speed modifier for every prefab
    public float defaultSpeedModifier = 1.0f;

    // Current movement speed modifier
    public float currentSpeedModifier = 0f;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UpdateSpeedModifier(defaultSpeedModifier);
        currentHealth = maxHealth;
        playerTransform = GameController.instance.player.transform;
        startingPosition = transform.position;

        // Check if there are children before accessing them
        if (transform.childCount > 0)
        {
            hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();

            // Set current health to max health at the start
            currentHealth = maxHealth;

            // Ignore collisions with the "MapCollider" layer initially
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("MapCollider"), true);
        }
    }

    protected void DealDamage(int damage)
    {
        // Assuming playerController is set correctly
        playerController = GameController.instance.player.GetComponent<PlayerController>();

        if (playerController != null)
        {
            // Calculate damage and call RecieveDamage in PlayerHealth
            //playerController.CalculatePlayerDamageTaken(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("triggerEnter2D with player");
            // Assuming you have a variable named damageAmount that represents the damage to be dealt
            int damageAmount = 10; // You can adjust this value

            // Call DealDamage to initiate the damage dealing process
            DealDamage(damageAmount);
        }
    }




    private void FixedUpdate()
    {
        // Always chase the player
        UpdateMotor((playerTransform.position - transform.position).normalized);

        // Enable collisions with the "MapCollider" layer
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("MapCollider"), false);
    }

    // Function to apply damage to the enemy
    public void TakeDamage(int dmg)
    {
        RecieveDamage(dmg);
    }
    protected override void RecieveDamage(int damage)
    {
            currentHealth -= damage; 

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
        }
    

    protected override void Death()
    {

        
        if (experienceDropPrefab != null)
        {
            // Instantiate the experience drop prefab at the enemy's position
            GameObject experienceDrop = Instantiate(experienceDropPrefab, transform.position, Quaternion.identity);
            playerController = GameController.instance.player.GetComponent<PlayerController>();
            if(playerController.DropDoubleExperience())
            {
                GameObject DoubledExperienceDrop = Instantiate(experienceDropPrefab, transform.position, Quaternion.identity);
            }
            // You can do additional setup or pass information to the experienceDrop here
        }
    

        // Destroy the enemy game object
        Destroy(gameObject);
        EnemySpawnController.currentEnemyCount--;
    }

    public void UpdateSpeedModifier(float modifier)
    {
        currentSpeedModifier += modifier;
        // No need for clamping if negative modifiers are allowed

        // Update the speed modifiers in the Mover class
        UpdateSpeedModifier(currentSpeedModifier, currentSpeedModifier);
    }
}
