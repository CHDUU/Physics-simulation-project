using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject pausePanel;

    public void Update()
    {
        if (pausePanel != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausePanel.SetActive(!pausePanel.activeSelf);
                Time.timeScale = pausePanel.activeSelf ? 0 : 1;
            }
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OptionsMenu()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("sceneName", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("OptionsMenu");
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
