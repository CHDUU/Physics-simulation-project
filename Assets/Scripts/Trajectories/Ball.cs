using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isStatic;
    public bool isReference;

    void Start()
    {
        if (isReference)
        {
            var shootScript = FindObjectOfType<ShootTrajectory>();
            shootScript.RegisterBall(this);
        }
    }
}
