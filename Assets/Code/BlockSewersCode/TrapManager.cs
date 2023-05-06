using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TrapManager : MonoBehaviour
{
    
    public string additiveSceneName;
    private GameManager gameManager;
    


    public Trap[] traps;
    int startingActiveTraps = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ActivateSomeTraps();
    }

    // Update is called once per frame
    void Update()
    {   
        // If all traps are activated - the minigame is over
        if(PublicVars.activeTraps == 7){
            EndGame();
            PublicVars.resetTraps();
            UnloadCurrentScene();
            // Check for any key event or click event to unload the current additive scene
        }
    }

    // player will start with 0-3 / 7 random active traps 
    public void ActivateSomeTraps() {
        // create an array of size 7
        bool[] trapToActivate = new bool[7];
        
        for (int i = 0; i < trapToActivate.Length; i++) {
            if(startingActiveTraps < 3){
                // generate random true or false value
                trapToActivate[i] = Random.value < 0.5f;
                if (trapToActivate[i]) {
                    // activate some traps
                    traps[i].Activate(true);
                    startingActiveTraps++;
                    PublicVars.activeTraps++;
                }
            }
        }
        print(startingActiveTraps);
    }

    // End the game and display the final score message
    private void EndGame()
    {
        // Calculate happiness change based on score and multiplier
        int happinessChange = 5;

        // Update happiness in the GameManager
        if (gameManager != null)
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
}
