using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mover
{
    private SpriteRenderer spriteRenderer;

    // Default movement speed modifier for every prefab
    private float defaultSpeedModifier = 1.0f;

    // Current movement speed modifier
    public float currentSpeedModifier = 0f;

    private float defaultDoubleExperienceModifier = 0f;
    public float currentDoubleExperienceModifier = 0f;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject);

        // Set the default modifier on start
        UpdateDoubleExperienceModifier(defaultDoubleExperienceModifier);
        UpdateSpeedModifier(defaultSpeedModifier);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    // Function to update the speed modifier additively
    public void UpdateDoubleExperienceModifier(float modifier)
    {
        currentDoubleExperienceModifier += modifier;
    }

    // Function to update the speed modifier additively
    public void UpdateSpeedModifier(float modifier)
    {
        currentSpeedModifier += modifier;
        // No need for clamping if negative modifiers are allowed

        // Update the speed modifiers in the Mover class
        UpdateSpeedModifier(currentSpeedModifier, currentSpeedModifier);
    }

    public bool DropDoubleExperience()
    {
        return Random.Range(0f, 1f) <= currentDoubleExperienceModifier;
    }
}
