using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public int score = 0;
    public int multiplier = 1;
    public Text scoreText;

    private int consecutiveShots = 0;
    private float comboResetTimer = 3f;
    private float currentTimer = 0f;

    void Update()
    {
        // Reset multiplier if time passes without scoring
        if (consecutiveShots > 0)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= comboResetTimer)
            {
                multiplier = 1;
                consecutiveShots = 0;
            }
        }
    }

    public void AddScore(int basePoints)
    {
        currentTimer = 0f; // Reset combo timer
        consecutiveShots++;

        // Increase multiplier for every 3 consecutive shots
        if (consecutiveShots % 3 == 0)
        {
            multiplier++;
        }

        score += basePoints * multiplier;
        scoreText.text = "Score: " + score;
    }
}
