using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GravitationalMotion : MonoBehaviour
{
    [Header("Components")]
    public LineRenderer lr;

    [Header("Field Variables")]
    public TMP_InputField massCenterField;
    public TMP_InputField massOrbitField;
    public TMP_InputField massCenterPowerField;
    public TMP_InputField massOrbitPowerField;
    public TMP_InputField radiusField;

    [Header("Text Variables")]
    public TextMeshProUGUI playText;
    public TextMeshProUGUI currentForceText;
    public TextMeshProUGUI currentPeriodText;

    [Header("GameObject")]
    public Transform orbitingObject;
    public Transform centralObject;


    private Circle orbitPath;
    private int segments;
    private float radius;
    private float massOfCenter;
    private float massOfOrbiting;
    private int massCenterPower;
    private int massOrbitPower;
    private const float AU = 149598000f;
    private const double G = .0000000000667f;
    private float orbitPeriod;
    private float force;
    private int forcePower;
    private float timeInterval;

    private Vector3[] points;
    private int currentPoint;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        currentPoint = 0;
        segments = 5000;

        CheckPlayerPrefs();
        InitSetVariables();
    }

    public void InitSetVariables()
    {
        if (!PlayerPrefs.HasKey("centerMass") || !PlayerPrefs.HasKey("orbitMass") || !PlayerPrefs.HasKey("centerPower") || !PlayerPrefs.HasKey("orbitPower") || !PlayerPrefs.HasKey("radius"))
        {
            return;
        }

        CalculateVariables();
        CalculateCircle();
        SetText();
    }

    public void SetVariables()
    {
        if (!float.TryParse(massCenterField.text, out float result) || !float.TryParse(massOrbitField.text, out float result2) || !int.TryParse(massCenterPowerField.text, out int result3) || !int.TryParse(massOrbitPowerField.text, out int result4) || !float.TryParse(radiusField.text, out float result5))
        {
            return;
        }

        massOfCenter = float.Parse(massCenterField.text);
        massOfOrbiting = float.Parse(massOrbitField.text);
        massCenterPower = int.Parse(massCenterPowerField.text);
        massOrbitPower = int.Parse(massOrbitPowerField.text);
        radius = float.Parse(radiusField.text);

        PlayerPrefs.SetFloat("centerMass", massOfCenter);
        PlayerPrefs.SetFloat("orbitMass", massOfOrbiting);
        PlayerPrefs.SetInt("centerPower", massCenterPower);
        PlayerPrefs.SetInt("orbitPower", massOrbitPower);
        PlayerPrefs.SetFloat("radius", radius);

        CalculateVariables();
        CalculateCircle();
        SetText();
    }

    void CalculateCircle()
    {
        points = new Vector3[segments + 1];
        for(int i = 0; i < segments; i++)
        {
            points[i] = orbitPath.Evaluate(((float)i / (float)segments), centralObject.position);
        }

        points[segments] = points[0];
        currentPoint = 0;
        orbitingObject.transform.position = points[currentPoint];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    void CalculateVariables()
    {
        orbitPath = new Circle(radius);
        orbitPeriod = Mathf.Sqrt((4 * Mathf.Pow(Mathf.PI, 2) * Mathf.Pow(radius*AU*Mathf.Pow(10,3), 3)) / (float)(G * massOfCenter * Mathf.Pow(10, massCenterPower)));
        orbitPeriod = (((orbitPeriod / 60f) / 60f) / 24f);
        force = (6.67f * massOfCenter * massOfOrbiting) / Mathf.Pow(radius * 1.49598f, 2);
        forcePower = 0;
        if(force > 10)
        {
            force /= 10;
            forcePower += 1;
        }
        if(force < 10)
        {
            force *= 10;
            forcePower -= 1;
        }
        forcePower += massCenterPower + massOrbitPower - 25;
        timeInterval = orbitPeriod / segments;
    }

    public void SetText()
    {
        currentForceText.text = "Current Force:\n" + Mathf.RoundToInt(force).ToString() + " x 10<sup>" + forcePower.ToString() + "</sup> N";
        currentPeriodText.text = "Current Period:\n" + Mathf.RoundToInt(currentPoint * timeInterval).ToString() + " / " + Mathf.RoundToInt(orbitPeriod).ToString() + " days";
    }

    public void Step()
    {
        if (points.Length == 0)
        {
            return;
        }

        currentPoint += 1;
        if (currentPoint >= points.Length - 1)
        {
            currentPoint = 0;
        }
        orbitingObject.transform.position = points[currentPoint];
        SetText();
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

    public void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("centerMass") && PlayerPrefs.HasKey("orbitMass") && PlayerPrefs.HasKey("centerPower") && PlayerPrefs.HasKey("orbitPower") && PlayerPrefs.HasKey("radius"))
        {
            massOfCenter = PlayerPrefs.GetFloat("centerMass");
            massOfOrbiting = PlayerPrefs.GetFloat("orbitMass");
            massCenterPower = PlayerPrefs.GetInt("centerPower");
            massOrbitPower = PlayerPrefs.GetInt("orbitPower");
            radius = PlayerPrefs.GetFloat("radius");

            massCenterField.text = massOfCenter.ToString();
            massOrbitField.text = massOfOrbiting.ToString();
            massCenterPowerField.text = massCenterPower.ToString();
            massOrbitPowerField.text = massOrbitPower.ToString();
            radiusField.text = radius.ToString();
        }
        else
        {
            return;
        }
    }
}