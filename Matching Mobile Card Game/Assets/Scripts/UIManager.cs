using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text timerText;
    public GameObject winPanel;
    public Text winTimeText;
    public Text bestTimeText;
    public Text startText;
    public Text moveCountText;
    public Text comboText;


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateTimer(float time)
    {
        timerText.text = "Time: " + time.ToString("F2");
    }

    public void ShowWinScreen(float time)
    {
        winPanel.SetActive(true);

        winTimeText.text = "Your Time: " + time.ToString("F2");

        float bestTime = PlayerPrefs.GetFloat("BestTime", time);
        bestTimeText.text = "Best Time: " + bestTime.ToString("F2");

        moveCountText.text = "Moves: " + GameManager.Instance.moveCount;
        comboText.text = "Max Combo: " + GameManager.Instance.maxCombo;
    }



    public void HideWinPanel()
    {
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    public void HideStartText()
    {
        if (startText != null)
            startText.gameObject.SetActive(false);
    }


}
