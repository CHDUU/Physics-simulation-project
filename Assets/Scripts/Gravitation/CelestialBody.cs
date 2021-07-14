using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float radius;
    public float SurfaceGravity;
    public float mass;
    public Vector3 initialVelocity;
    public Vector3 velocity;
    public string CelestialBodyName = "Please Provide Name";
    Transform mesh;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        velocity = initialVelocity;

    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep)
    {
        foreach(var otherBody in allBodies)
        {
            if (otherBody != this)
            {
                float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDirection = (otherBody.rb.position - rb.position).normalized;
                Vector3 acceleration = forceDirection * Universe.gravitationalConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }
    
    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep)
    {
        rb.MovePosition(rb.position + velocity * timeStep);
    }

    void OnValidate()
    {
        mass = SurfaceGravity * Mathf.Pow(radius, 2) / Universe.gravitationalConstant;
        mesh = transform.GetChild(0);
        mesh.localScale = Vector3.one * radius;
        gameObject.name = CelestialBodyName;
    }

    public Vector3 Position
    {
        get
        {
            return rb.position;
        }
    }

    public Rigidbody Rigidbody
    {
        get
        {
            return rb;
        }
    }
}
