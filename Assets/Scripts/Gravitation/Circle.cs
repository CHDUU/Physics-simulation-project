using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Circle
{
    public float radius;

    public Circle(float radius)
    {
        this.radius = radius*3;
    }

    public Vector2 Evaluate(float t, Vector2 center)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * radius + center.x;
        float y = Mathf.Cos(angle) * radius + center.y;
        return new Vector2(x, y);
    }
}
