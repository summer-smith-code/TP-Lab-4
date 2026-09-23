using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth : MonoBehaviour
{
    [SerializeField] private int health;
    void Start()
    {
        if (health < 1)
        {
            Debug.Log("0 health on spawn!");
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        CollisionDetection.OnCollision += HealthCheck;
    }

    private void OnDisable()
    {
        CollisionDetection.OnCollision -= HealthCheck;
    }

    private void HealthCheck(GameObject object1, GameObject object2)
    {
        if (this.gameObject == object1)
        {
            if (object2.tag == "Laser")
            {
                health -= 1;
                if (health <= 0)
                {
                    Destroy(this);
                }
            }
        }
        else return;
    }
}
