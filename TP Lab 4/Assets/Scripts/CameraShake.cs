using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineImpulseSource _impulseSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        _impulseSource = this.gameObject.GetComponent<CinemachineImpulseSource>();
        MeteorHealth.OnMeteorDeath += Impulse;
    }

    private void OnDisable()
    {
       MeteorHealth.OnMeteorDeath -= Impulse;
    }

    private void Impulse(int obj)
    {
        Debug.Log("Impulse check");
        if (_impulseSource != null)
        {
            Debug.Log("IMPULSING!");
            _impulseSource.GenerateImpulse();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
