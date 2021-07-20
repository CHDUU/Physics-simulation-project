using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void Start()
    {
        if (PlayerPrefs.HasKey("resWidth") && PlayerPrefs.HasKey("resHeight"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("resWidth"), PlayerPrefs.GetInt("resHeight"), Screen.fullScreen);
        }
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            if(PlayerPrefs.GetInt("isFullscreen") == 1)
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
            }
        }
        if (PlayerPrefs.HasKey("qualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
        }
        if (PlayerPrefs.HasKey("volumeLevel"))
        {
            audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volumeLevel"));
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OptionsScene()
    {
        PlayerPrefs.SetString("sceneName", "MainMenu");
        SceneManager.LoadScene("OptionsMenu");
    }

    public void GravitationScene()
    {
        SceneManager.LoadScene("GravitationScene");
    }

    public void TrajectoryScene()
    {
        SceneManager.LoadScene("TrajectoryScene");
    }

    public void CollisionScene()
    {
        SceneManager.LoadScene("CollisionScene");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
