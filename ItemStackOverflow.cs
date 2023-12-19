using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStackOverflow : MonoBehaviour
{
    public void LevelUpStackOverflow()
    {
        // Find the PlayerController component on the GameObject this script is attached to
        PlayerController playerControllerInstance = GetComponent<PlayerController>();

        // Check if the component was found before calling the method
        if (playerControllerInstance != null)
        {
            playerControllerInstance.UpdateDoubleExperienceModifier(0.1f);
        }
    }
}
