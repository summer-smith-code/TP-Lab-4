using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth : MonoBehaviour
{
    public static event Action<int> OnMeteorDeath;
    [SerializeField] private int health;
    private CinemachineImpulseSource _impulseSource;

    void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
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
        // check if its this object
        if (this.gameObject == object1)
        {
            //check if the other object is the laser
            if (object2.tag == "Laser")
            {
                Debug.Log("TRIGGER LASER!");
                health -= 1;
                // destroy laser
                Destroy(object2);
                if (health <= 0)
                {
                    OnMeteorDeath?.Invoke(1);
                    if (_impulseSource != null) _impulseSource.GenerateImpulse();
                    // destroy this meteor
                    Destroy(this.gameObject);
                }
            }
        }
        else return;
    }
}
