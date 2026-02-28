using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float timer = 1f;
    private bool isPlaying = true;

    public int currentLevel = 1; 
    public int[] cardsPerLevel = { 4, 8, 12 }; 

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

        // Save best time
        float bestTime = PlayerPrefs.GetFloat("BestTime", 9999f);
        if (timer < bestTime)
            PlayerPrefs.SetFloat("BestTime", timer);

        // Show Win Panel
        if (UIManager.Instance != null)
            UIManager.Instance.ShowWinScreen(timer);

        // Move to next level if exists
        if (currentLevel < cardsPerLevel.Length)
        {
            currentLevel++;
            Invoke(nameof(LoadNextLevel), 1f); // wait 1 second before next level
        }
    }

    void LoadNextLevel()
    {
        BoardManager.Instance.GenerateBoard(currentLevel);
        timer = 0f;
        isPlaying = true;

        UIManager.Instance.HideWinPanel();
    }



    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }


    private bool isPaused = false;

    public void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f; 
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1f; 
            isPaused = false;
        }
    }


  



}
