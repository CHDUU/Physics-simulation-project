using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour
{
    public LineRenderer lr;

    [Range(5, 40)]
    public int segments;
    public Ellipse ellipse;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];
        for(int i = 0; i < segments; i++)
        {
            points[i] = ellipse.Evaluate(((float)i / (float)segments));
        }

        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if(Application.isPlaying && lr != null)
            CalculateEllipse();
    }
}
