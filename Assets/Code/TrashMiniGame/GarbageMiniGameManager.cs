using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GarbageMiniGameManager : MonoBehaviour
{
    public int score;
    // Reference to the TextMeshProUGUI components
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalScoreText;
    public string additiveSceneName;
    public GarbageSpawner garbageSpawner;

    // Timer and game state
    public float remainingTime = 15f; // 15 seconds
    private bool gameEnded = false;

    private GameManager gameManager;

    // Multiplier for calculating happiness change
    public int happinessMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        score = 0; // Initialize the score to 0
        UpdateScoreText(); // Update the score text
        finalScoreText.gameObject.SetActive(false); // Hide the final score text
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameEnded)
        {
            // Update the timer
            remainingTime -= Time.deltaTime;
            UpdateTimerText();
            UpdateScoreText();

            // Check if the timer has reached zero
            if (remainingTime <= 0f)
            {
                EndGame();
            }
        }
        else
        {
            // Check for any key event or click event to unload the current additive scene
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                UnloadCurrentScene();
            }
        }
    }

    // Public method to get the current score
    public int GetScore()
    {
        return score;
    }

    // Public method to increment the score
    public void IncrementScore(int amount)
    {
        // Debug when called
        score += amount;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Garbage collected: {score}";
        }
    }

    // Update the score text to display the current score
    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int remainingSeconds = Mathf.Max(0, Mathf.FloorToInt(remainingTime));
            timerText.text = $"Time remaining: {remainingSeconds}s";
        }
    }

    // End the game and display the final score message
    private void EndGame()
    {
        gameEnded = true;
        finalScoreText.gameObject.SetActive(true);
        finalScoreText.text = $"You picked up {score} pieces of trash! Good job!";

        // Stop spawning squares and destroy all spawned squares
        if (garbageSpawner != null)
        {
            garbageSpawner.ToggleSpawning(false);
            garbageSpawner.DestroyAllSquares();
        }

        // Calculate happiness change based on score and multiplier
        int happinessChange = (score * happinessMultiplier) - 5;

        // Update happiness in the GameManager
        if (gameManager != null )
        {
            gameManager.UpdateHappiness(happinessChange);
        }
    }


    // Unload the current additive scene and return to the main scene
    private void UnloadCurrentScene()
    {
        if (!string.IsNullOrEmpty(additiveSceneName))
        {
            // Unload the current additive scene
            SceneManager.UnloadSceneAsync(additiveSceneName);
            additiveSceneName = null;

            // Call the ReactivatePlayerAndUI function from the GameManager script
            if (gameManager != null)
            {
                gameManager.ReactivatePlayerAndUI();
            }
        }
    }

    void OnDestroy()
    {
        garbageSpawner.DestroyAllSquares();

    }
}