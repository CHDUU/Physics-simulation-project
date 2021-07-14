using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBehavior : MonoBehaviour
{
    public TextMeshProUGUI text;
    private bool hasStarted = false;

    public void Run()
    {
        if (hasStarted){
            hasStarted = false;
            Time.timeScale = 0;
            text.text = "Stop";
        }
        else
        {
            hasStarted = true;
            Time.timeScale = 1;
            text.text = "Start";
        }
    }
}
