using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float timer = 0f;
    private bool isPlaying = true;

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
}
