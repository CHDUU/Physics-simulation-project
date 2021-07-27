using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collisions : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody rb1;
    public Rigidbody rb2;

    [Header("Field Variables")]
    public TMP_InputField mass1Field;
    public TMP_InputField vel1Field;
    public TMP_InputField mass2Field;
    public TMP_InputField vel2Field;

    [Header("Toggle Variable")]
    public Toggle elasticToggle;

    [Header("Text Variables")]
    public TextMeshProUGUI playText;
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI momentumText;
    public TextMeshProUGUI kineticEnergyText;

    [Header("GameObject")]
    public GameObject cube1;
    public GameObject cube2;
    public PhysicMaterial material;


    private float mass1;
    private float vel1;
    private float mass2;
    private float vel2;
    private float momentum;
    private float kineticEnergy;
    private bool isElastic;
    private bool isPlaying;
    private float timer;



    void Start()
    {
        CheckPlayerPrefs();
        InitSetVariables();
    }

    public void InitSetVariables()
    {
        if (!PlayerPrefs.HasKey("mass1") || !PlayerPrefs.HasKey("mass2") || !PlayerPrefs.HasKey("vel1") || !PlayerPrefs.HasKey("vel2") || !PlayerPrefs.HasKey("elastic"))
        {
            return;
        }

        cube1.transform.position = new Vector2(-4, 0);
        cube2.transform.position = new Vector2(4, 0);
        rb1.mass = mass1;
        rb2.mass = mass2;

        if (isElastic)
        {
            material.bounciness = 1f;
        }
        else
        {
            material.bounciness = 0f;
        }

        CalculateVariables();
        SetText();
    }

    void CalculateVariables()
    {
        kineticEnergy = (0.5f * mass1 * Mathf.Pow(vel1,2)) + (0.5f * mass2 * Mathf.Pow(vel2, 2));
        momentum = (mass1*vel1) + (mass2 * vel2);
    }

    public void SetVariables()
    {
        if (!float.TryParse(mass1Field.text, out float result) || !float.TryParse(mass2Field.text, out float result2) || !float.TryParse(vel1Field.text, out float result3) || !float.TryParse(vel2Field.text, out float result4))
        {
            return;
        }

        mass1 = float.Parse(mass1Field.text);
        mass2 = float.Parse(mass2Field.text);
        vel1 = float.Parse(vel1Field.text);
        vel2 = float.Parse(vel2Field.text);
        isElastic = elasticToggle.isOn;

        PlayerPrefs.SetFloat("mass1", mass1);
        PlayerPrefs.SetFloat("mass2", mass2);
        PlayerPrefs.SetFloat("vel1", vel1);
        PlayerPrefs.SetFloat("vel2", vel2);
        PlayerPrefs.SetString("elastic", isElastic.ToString());

        if (isElastic)
        {
            material.bounciness = 1f;
        }
        else
        {
            material.bounciness = 0f;
        }


        cube1.transform.position = new Vector2(-4, 0);
        cube2.transform.position = new Vector2(4, 0);
        rb1.mass = mass1;
        rb2.mass = mass2;

        CalculateVariables();
        SetText();
    }

    public void PlayOrPause()
    {
        if (!PlayerPrefs.HasKey("mass1") || !PlayerPrefs.HasKey("mass2") || !PlayerPrefs.HasKey("vel1") || !PlayerPrefs.HasKey("vel2") || !PlayerPrefs.HasKey("elastic"))
        {
            return;
        }


        if (playText.text.Equals("Play"))
        {
            playText.text = "Stop";
            rb1.velocity = new Vector2(vel1, 0);
            rb2.velocity = new Vector2(vel2, 0);
            isPlaying = true;
        }
        else
        {
            playText.text = "Play";
            timer = 0f;
            cube1.transform.position = new Vector2(-4, 0);
            cube2.transform.position = new Vector2(4, 0);
            rb1.velocity = new Vector2(0, 0);
            rb2.velocity = new Vector2(0, 0);
            SetText();
            isPlaying = false;
        }
    }

    public void SetText()
    {
        
        momentumText.text = "Momentum:\n" + momentum.ToString() + " kg m/s";
        kineticEnergyText.text = "Kinetic Energy:\n" + kineticEnergy.ToString() + " J";
        currentTimeText.text = "Current Time:\n" + timer.ToString("00.00") + " s";

    }

    public void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("mass1") && PlayerPrefs.HasKey("mass2") && PlayerPrefs.HasKey("vel1") && PlayerPrefs.HasKey("vel2") && PlayerPrefs.HasKey("elastic"))
        {
            mass1 = PlayerPrefs.GetFloat("mass1");
            mass2 = PlayerPrefs.GetFloat("mass2");
            vel1 = PlayerPrefs.GetFloat("vel1");
            vel2 = PlayerPrefs.GetFloat("vel2");
            isElastic = ToBool(PlayerPrefs.GetString("elastic"));

            mass1Field.text = mass1.ToString();
            mass2Field.text = mass2.ToString();
            vel1Field.text = vel1.ToString();
            vel2Field.text = vel2.ToString();
            elasticToggle.isOn = isElastic;

        }
        else
        {
            return;
        }
    }

    public static bool ToBool(string test)
    {
        return test == "true" || test == "on" || test == "yes" || test == "True";
    }

    public void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            SetText();
        }
    }
}
