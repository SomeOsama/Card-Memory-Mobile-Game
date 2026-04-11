using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    AudioSource button;

    private void Start()
    {
        button = GetComponent<AudioSource>();
    }

    public void Menu()
    {
        button.Play();
        SoundDelay();
        SceneManager.LoadScene("Menu");
    }
    public void Play()
    {
        button.Play();
        SoundDelay();
        SceneManager.LoadScene("Main");
    }

    public void Settings()
    {
        button.Play();
        SoundDelay();
        SceneManager.LoadScene("Settings");
    }
    public void Credits()
    {
        button.Play();
        SoundDelay();
        SceneManager.LoadScene("Credits");
    }
    public void Exit()
    {
        button.Play();
        SoundDelay();
        Application.Quit();
    }
    
    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(.75f);
    }
}



