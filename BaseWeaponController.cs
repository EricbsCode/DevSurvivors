// BaseWeaponController.cs
using UnityEngine;

public abstract class BaseWeaponController : MonoBehaviour
{
    public int damage = 1;
    public float cooldown = 1.0f;
    private float cooldownTimer;

    protected virtual void Update()
    {
        // Update cooldown timer
        cooldownTimer -= Time.deltaTime;

        // Check if the weapon can fire
        if (cooldownTimer <= 0)
        {
            PerformAction();
            cooldownTimer = cooldown;
        }
    }

    protected abstract void PerformAction();
}
