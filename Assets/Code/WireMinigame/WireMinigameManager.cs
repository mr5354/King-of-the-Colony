using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WireMiniGameManager : MonoBehaviour
{
    public GameObject[] wires;
    public GameObject wire = null;
    public GameObject indicator;
    public GameObject wirePanel;
    public GameObject gameText;
    public GameObject finalTextPanel;

    private int correctWireIndex;
    private int score = 0;
    private int currRound = 1;
    private int maxRound = 6;
    private GameManager gameManager;
    public string additiveSceneName;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI finalText;

    private bool gameEnded = false;

    private void Start()
    {
        finalTextPanel.SetActive(false);
        ShuffleWires();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (currRound < maxRound && !gameEnded)
        {
            foreach(GameObject obj in wires) {
                if(!obj.activeSelf) {
                    // Hidden object is found, do something
                    Debug.Log("Found the hidden object!");
                    wire = obj;
                    Debug.Log("Hidden" + wire);
                    Debug.Log(wires[correctWireIndex]);
                }
            }
            if (wire != null)
            {
                wire.SetActive(true);
                if (wire == wires[correctWireIndex])
                {
                    // Correct wire was clicked
                    score++;
                    scoreText.text = "Score: " + score.ToString();
                    Debug.Log("right");
                    ShuffleWires();
                }
                else
                {
                    // Incorrect wire was clicked
                    Debug.Log("wrong");
                    ShuffleWires();
                }
                
                currRound++;
                roundText.text = "Round " + currRound.ToString();
                
                wire = null;
            }
            if(currRound >= maxRound){
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

    private void ShuffleWires()
    {
        // Shuffle the wires array
        for (int i = 0; i < wires.Length; i++)
        {
            GameObject temp = wires[i];
            int randomIndex = Random.Range(i, wires.Length);
            wires[i] = wires[randomIndex];
            wires[randomIndex] = temp;
        }

        // Set the wire colors
        List<Color> availableColors = new List<Color>{Color.red, Color.green, Color.blue, Color.white};
        for (int i = 0; i < wires.Length; i++)
        {
            // Choose a random color
            int randomColorIndex = Random.Range(0, availableColors.Count);
            Color randomColor = availableColors[randomColorIndex];
            // Set the wire color
            wires[i].GetComponent<Renderer>().material.color = randomColor;
            // Remove the color from the available colors
            availableColors.RemoveAt(randomColorIndex);
        }

        // Choose a random wire to be the correct one
        correctWireIndex = Random.Range(0, wires.Length);
        Debug.Log(correctWireIndex);

        // Set the indicator colors to match the correct wire
        indicator.GetComponent<Renderer>().material.color = wires[correctWireIndex].GetComponent<Renderer>().material.color;
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
    private void EndGame()
    {
        gameEnded = true;

        wirePanel.SetActive(false);
        gameText.SetActive(false);
        finalText.text = "You Scored: " + score.ToString();
        finalTextPanel.SetActive(true);

        // Calculate happiness change based on score and multiplier
        int happinessChange = score;

        // Update happiness in the GameManager
        if (gameManager != null)
        {
            print(happinessChange);
            gameManager.UpdateHappiness(happinessChange);
        }
    }
}
