using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerDeath;
    // Start is called before the first frame update
    private void OnEnable()
    {
        CollisionDetection.OnCollision += CheckHealth;
    }

    private void OnDisable()
    {
        CollisionDetection.OnCollision -= CheckHealth;
    }

    private void CheckHealth(GameObject object1, GameObject object2)
    {
        // check if the collider was the player
        if (object1 == this.gameObject)
        {
            // player dies!
            OnPlayerDeath(object1);
        }
    }
}
