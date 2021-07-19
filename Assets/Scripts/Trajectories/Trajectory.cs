using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trajectory : MonoBehaviour
{
    [Header("Components")]
    public LineRenderer lr;

    [Header("Field Variables")]
    public TMP_InputField angleField;
    public TMP_InputField heightField;
    public TMP_InputField velInitField;
    public TMP_InputField accelerationField;
    public TMP_InputField timeIntField;
    public TMP_InputField totalTimeField;

    [Header("Text Variables")]
    public TextMeshProUGUI playText;
    public TextMeshProUGUI currentTime;
    public TextMeshProUGUI currentPosition;
    public TextMeshProUGUI currentVelocity;
    
    [Header("GameObject")]
    public GameObject sphere;

    private Vector3[] points;
    private float angle;
    private float height;
    private float velInit;
    private float acceleration;
    private float timeInterval;
    private float totalTime;
    private float velXInit;
    private float velYInit;
    private int segments;
    private float currVel;

    private int currentPoint;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        currentPoint = 0;
    }

    public void SetVariables()
    {
        if(!float.TryParse(angleField.text, out float result) || !float.TryParse(heightField.text, out float result2) || !float.TryParse(velInitField.text, out float result3) || !float.TryParse(accelerationField.text, out float result4) || !float.TryParse(timeIntField.text, out float result5) || !float.TryParse(totalTimeField.text, out float result6))
        {
            return;
        }

        angle = float.Parse(angleField.text);
        height = float.Parse(heightField.text);
        velInit = float.Parse(velInitField.text);
        acceleration = float.Parse(accelerationField.text);
        timeInterval = float.Parse(timeIntField.text);
        totalTime = float.Parse(totalTimeField.text);

        segments = (int)(totalTime / timeInterval);
        points = new Vector3[segments];
        float x = 0;
        float y = 0;

        velXInit = velInit * Mathf.Cos(Mathf.Deg2Rad * angle);
        velYInit = velInit * Mathf.Sin(Mathf.Deg2Rad * angle);

        for (int i = 0; i < segments; i++)
        {
            x = velXInit * timeInterval * i;
            y = (velYInit * timeInterval * i) + height + (0.5f * acceleration * Mathf.Pow(timeInterval * i, 2));
            points[i] = new Vector3(0.1f * x, 0.1f * y, 0);
        }

        currentPoint = 0;
        sphere.transform.position = points[currentPoint];

        lr.positionCount = segments;
        lr.SetPositions(points);
    }

    public void Step()
    {
        if(points.Length == 0)
        {
            return;
        }

        currentPoint += 1;
        if(currentPoint >= points.Length - 1)
        {
            currentPoint = 0;
        }
        sphere.transform.position = points[currentPoint];
        currentPosition.text = "Current Position:\n" + new Vector2(points[currentPoint].x, points[currentPoint].y).ToString() + " m";

        if (currentPoint > 0) 
        {
            if (points[currentPoint].y > points[currentPoint - 1].y)
            {
                currVel = Mathf.Sqrt(Mathf.Pow((points[currentPoint].x - points[currentPoint - 1].x) / timeInterval, 2) + Mathf.Pow((points[currentPoint].y - points[currentPoint - 1].y) / timeInterval, 2));
            }
            else
            {
                currVel = -Mathf.Sqrt(Mathf.Pow((points[currentPoint].x - points[currentPoint - 1].x) / timeInterval, 2) + Mathf.Pow((points[currentPoint].y - points[currentPoint - 1].y) / timeInterval, 2));
            }
        }
        else 
        {
            currVel = velInit; 
        }

        currentVelocity.text = "Current Velocity:\n" + currVel.ToString("00.00") + " m/s";
        currentTime.text = "Current Time:\n" + (currentPoint * timeInterval).ToString("00.00") + " s";
        
    }

    public void PlayOrPause()
    {
        if (points.Length == 0)
        {
            return;
        }

        if (playText.text.Equals("Play"))
        {
            playText.text = "Pause";
            InvokeRepeating("Step", 0, timeInterval);
        }
        else
        {
            playText.text = "Play";
            CancelInvoke();
        }
        
        
        
    }

}
