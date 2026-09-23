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

}
