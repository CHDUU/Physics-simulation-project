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

    private int resValue;
    private int resWidth;
    private int resHeight;
    private int isFullscreen;
    private int qualityLevel;
    private float volumeLevel;


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

        if (PlayerPrefs.HasKey("resValue"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("resValue");
            resolutionDropdown.RefreshShownValue();
            resValue = PlayerPrefs.GetInt("resValue");
            resWidth = PlayerPrefs.GetInt("resWidth");
            resHeight = PlayerPrefs.GetInt("resHeight");
        }
        else
        {
            resolutionDropdown.value = resIndex;
            resolutionDropdown.RefreshShownValue();
        }


        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            isFullscreen = PlayerPrefs.GetInt("isFullscreen");
            if (PlayerPrefs.GetInt("isFullscreen") == 1)
                fullscreenToggle.isOn = true;
            else
                fullscreenToggle.isOn = false;
        }

        if (PlayerPrefs.HasKey("qualityLevel"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("qualityLevel");
            qualityDropdown.RefreshShownValue();
            qualityLevel = PlayerPrefs.GetInt("qualityLevel");
        }

        if (PlayerPrefs.HasKey("volumeLevel"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volumeLevel");
            volumeLevel = PlayerPrefs.GetFloat("volumeLevel");
        }


        resolutionDropdown.onValueChanged.AddListener((value) =>
        {
            resWidth = resolutions[value].width;
            resHeight = resolutions[value].height;
            Screen.SetResolution(resWidth, resHeight, Screen.fullScreen);
            resValue = value;
        });

        volumeSlider.onValueChanged.AddListener((value) =>
        {
            audioMixer.SetFloat("volume", value);
            volumeLevel = value;
        });

        qualityDropdown.onValueChanged.AddListener((value) =>
        {
            QualitySettings.SetQualityLevel(value);
            qualityLevel = value;
        });

        fullscreenToggle.onValueChanged.AddListener((value) =>
        {
            Screen.fullScreen = value;
            if (value)
            {
                isFullscreen = 1;
            }
            else
            {
                isFullscreen = 0;
            }
        });

    }

    public void BackButton()
    {
        SetPlayerPrefs();
        SceneManager.LoadScene(PlayerPrefs.GetString("sceneName"));
    }

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("resValue", resValue);
        PlayerPrefs.SetInt("resWidth", resWidth);
        PlayerPrefs.SetInt("resHeight", resHeight);
        PlayerPrefs.SetInt("isFullscreen", isFullscreen);
        PlayerPrefs.SetInt("qualityLevel", qualityLevel);
        PlayerPrefs.SetFloat("volumeLevel", volumeLevel);
    }
}
