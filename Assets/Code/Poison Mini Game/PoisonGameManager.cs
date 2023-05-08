using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PoisonGameManager : MonoBehaviour
{
    public static int score;
    private bool gameEnded = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalScoreText;

    public float remainingTime = 15f; // 15 seconds

    private GameManager gameManager;
    public string additiveSceneName;

    //private bool isSpawning = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        score = 0; // Initialize the score to 0
        UpdateScoreText(); // Update the score text
        finalScoreText.gameObject.SetActive(false); // Hide the final score text
        Spawner.ToggleSpawning(true);
    }

    // Update is called once per frame
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
    // End the game and display the final score message
    private void EndGame()
    {
        gameEnded = true;
        finalScoreText.gameObject.SetActive(true);
        finalScoreText.text = $"YOUR SCORE WAS {score}";

        // Calculate happiness change based on score and multiplier
        int happinessChange = score;

        Spawner.ToggleSpawning(false);

        // Update happiness in the GameManager
        if (gameManager != null)
        {
            print(happinessChange);
            gameManager.UpdateHappiness(happinessChange);
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
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Cheese poisoned: {score}";
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
}
