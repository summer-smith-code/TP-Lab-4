using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Invoked in the order of this object, other object
    public static event Action<GameObject, GameObject> OnCollision;
    private void OnTriggerEnter(Collider other)
    {
        OnCollision?.Invoke(this.gameObject, other.gameObject);
    }
}
