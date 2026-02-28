using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("DifficultySelect");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
