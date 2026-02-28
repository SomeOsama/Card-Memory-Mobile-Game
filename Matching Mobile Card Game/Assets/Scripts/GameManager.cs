using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float timer = 1f;
    private bool isPlaying = true;

    private void Start()
    {
        Time.timeScale = 1f; 
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!isPlaying) return;

        timer += Time.deltaTime;
        UIManager.Instance.UpdateTimer(timer);
    }

    public void LevelComplete()
    {
        isPlaying = false;

        float bestTime = PlayerPrefs.GetFloat("BestTime", 9999f);

        if (timer < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", timer);
        }

        UIManager.Instance.ShowWinScreen(timer);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }


}
