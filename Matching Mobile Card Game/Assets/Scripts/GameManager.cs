using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float timer = 0f;
    private bool isPlaying = false;
    public int moveCount = 0;
    public int comboCount = 0;
    public int maxCombo = 0;


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

    private bool waitingForNextLevel = false;

    private void Update()
    {
        if (Pause.Instance != null && Pause.Instance.IsPaused) 
        {
            return;
        }

        if (!isPlaying && !waitingForNextLevel)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
            return;
        }

        if (waitingForNextLevel)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LoadNextLevel();
            }
            return;
        }

        timer += Time.deltaTime;
        UIManager.Instance.UpdateTimer(timer);
    }


    void StartGame()
    {
        isPlaying = true;
        timer = 0f;
        moveCount = 0;
        comboCount = 0;
        maxCombo = 0;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.HideStartText();
        }            
       
    }



    public void LevelComplete()
    {
        isPlaying = false;
        waitingForNextLevel = true;

        float bestTime = PlayerPrefs.GetFloat("BestTime", 9999f);
        if (timer < bestTime)
            PlayerPrefs.SetFloat("BestTime", timer);

        if (UIManager.Instance != null)
            UIManager.Instance.ShowWinScreen(timer);
    }


    void LoadNextLevel()
    {
        waitingForNextLevel = false;

        if (currentLevel < cardsPerLevel.Length)
            currentLevel++;

        BoardManager.Instance.GenerateBoard(currentLevel);

        timer = 0f;
        isPlaying = false; 

        if (UIManager.Instance != null)
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

    public void SetPausedState(bool paused)
    {
        isPaused = paused;
    }

    public void RegisterMove()
    {
        moveCount++;
    }

    public void RegisterMatch()
    {
        comboCount++;

        if (comboCount > 1) 
        {
            timer -= 3f;
            if (timer < 0) timer = 0;
        }

        if (comboCount > maxCombo)
            maxCombo = comboCount;
    }

    public void ResetCombo()
    {
        comboCount = 0;
    }

}
