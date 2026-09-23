using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeteor
{
    float distanceSquared { get; set; }
    bool isMovingLeft { get; set; }
    float speed { get; set; }
    Vector3 direction { get; set; }
    void Move();
}
