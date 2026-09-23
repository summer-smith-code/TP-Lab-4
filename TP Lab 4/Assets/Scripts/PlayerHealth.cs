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

    private void CheckHealth(GameObject object1, GameObject object2)
    {
        if (object1 == this.gameObject)
        {
            OnPlayerDeath(object1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
