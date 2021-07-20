using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    public Slider volumeSlider;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public AudioMixer audioMixer;
    private Resolution[] resolutions;
    private int resIndex;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener((value) =>
        {
            Screen.SetResolution(resolutions[value].width, resolutions[value].height, Screen.fullScreen);
        });

        volumeSlider.onValueChanged.AddListener((value) =>
        {
            audioMixer.SetFloat("volume", value);
        });

        qualityDropdown.onValueChanged.AddListener((value) =>
        {
            QualitySettings.SetQualityLevel(value);
        });

        fullscreenToggle.onValueChanged.AddListener((value) =>
        {
            Screen.fullScreen = value;
        });

    }
}
