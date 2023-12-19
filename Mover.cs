using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : FighterController
{
    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hit;
    protected Vector3 moveDelta;
    protected float baseYSpeed = 10.0f;
    protected float baseXSpeed = 14.0f;
    protected float ySpeedModifier = 1.0f; // Default modifier
    protected float xSpeedModifier = 1.0f; // Default modifier

    protected void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Apply modifiers to base speeds
        float currentYSpeed = baseYSpeed * ySpeedModifier;
        float currentXSpeed = baseXSpeed * xSpeedModifier;

        moveDelta = new Vector3(input.x * currentXSpeed, input.y * currentYSpeed, 0);

        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Check for collisions with the map collider
        CheckMapCollision();

        // Apply movement
        transform.Translate(moveDelta * Time.deltaTime);
    }

    // Function to check collisions with the map collider
    protected void CheckMapCollision()
    {
        // Check if the calling object has the tag "Player"
        if (gameObject.CompareTag("Player"))
        {
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("MapCollider"));

            if (hit.collider != null)
            {
                // If there is a collision in the y-axis, stop vertical movement
                moveDelta.y = 0;
            }

            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("MapCollider"));

            if (hit.collider != null)
            {
                // If there is a collision in the x-axis, stop horizontal movement
                moveDelta.x = 0;
            }
        }
    }

    // Function to update speed modifiers additively
    public void UpdateSpeedModifier(float xSpdModifier, float ySpdModifier)
    {
        xSpeedModifier += xSpdModifier;
        ySpeedModifier += ySpdModifier;
    }
}
