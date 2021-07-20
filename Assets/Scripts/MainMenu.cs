using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartScene()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OptionsScene()
    {
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
