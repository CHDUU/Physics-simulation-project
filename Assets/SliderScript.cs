using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public TMP_InputField input;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((value)=>
        {
            value = Mathf.RoundToInt(value * 100f) * 0.01f;
            input.text = value.ToString();
        });

        input.onValueChanged.AddListener((value)=> 
        {
            if(!float.TryParse(input.text, out float result))
            {
                return;
            }
            if(float.Parse(input.text) > slider.maxValue)
            {
                slider.value = slider.maxValue;
            }
            else if (float.Parse(input.text) < slider.minValue)
            {
                slider.value = slider.minValue;
            }
            else
            {
                slider.value = float.Parse(input.text);
            }
        });
    }
}
