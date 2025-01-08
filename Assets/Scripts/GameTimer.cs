using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public Text timerText;       // UI Text for displaying the timer
    public GameObject gameOverPanel; // Panel to show when the timer runs out

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        totalTime -= Time.deltaTime;

        // Update the timer UI
        timerText.text = Mathf.Max(totalTime, 0).ToString("F0") + "s";

        // Check if time is up
        if (totalTime <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }
}
