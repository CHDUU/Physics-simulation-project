using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public LineRenderer lr;

    private Vector3[] points;

    [Range(0,90)]
    public float angle;
    [Range(0,10000)]
    public float height;
    [Range(0,10000)]
    public float velInit;
    public float gravity;
    [Range(0,1)]
    public float timeInterval;
    [Range(0,100)]
    public float totalTime;

    private float velXInit;
    private float velYInit;
    private int segments;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void OnValidate()
    {
        segments = (int)(totalTime/timeInterval);
        points = new Vector3[segments];
        float x = 0;
        float y = 0;

        velXInit = velInit * Mathf.Cos(Mathf.Deg2Rad * angle);
        velYInit = velInit * Mathf.Sin(Mathf.Deg2Rad * angle);

        for (int i = 0; i < segments; i++)
        {
            x = velXInit * timeInterval * i;
            y = (velYInit * timeInterval * i) + height + (0.5f * gravity * Mathf.Pow(timeInterval * i, 2));
            points[i] = new Vector3(0.1f*x, 0.1f*y, 0);
        }

        lr.positionCount = segments;
        lr.SetPositions(points);
    }
}
