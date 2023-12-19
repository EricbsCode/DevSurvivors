using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    protected int hitpoint;
    protected int maxHitpoint = 100;
    public float pushRecoverySpeed = 0.2f;

    protected float immuneTime = 1.0f;
    protected float lastImmune;

    protected Vector3 pushDirection;

    protected virtual void RecieveDamage(int dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg; 

            if(hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }

    }
    
    protected virtual void Death()
    {

    }


}
