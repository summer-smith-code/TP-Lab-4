using System;
using UnityEngine;

// This script handles the player's health.
public class PlayerHealth : MonoBehaviour
{
    // Event which triggers upon the game object dying.
    public static event Action<GameObject> OnDeath;
    
    // Collision detection subscribes to CheckHealth().
    private void OnEnable()
    {
        CollisionDetection.OnCollision += CheckHealth;
    }

    // Collision detection unsubscribes to CheckHealth().
    private void OnDisable()
    {
        CollisionDetection.OnCollision -= CheckHealth;
    }

    // Checks the health of the given game object and determines if death event should be called.
    private void CheckHealth(GameObject object1, GameObject object2)
    {
        // If one of the objects in the collision was this game object, call death event.
        if (object1 == this.gameObject) OnDeath(object1);
    }
}
