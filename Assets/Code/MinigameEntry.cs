using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEntry : MonoBehaviour
{
    public float interactionDistance;
    private float showHighlightDistance = 11f;
    public string sceneToLoad;
    public GameObject highlight;
    private float distance;
    public bool active = false;
    public string name;
    public GameObject UIHighlight;
    public GameObject Player;
    public GameManager gameManager; // Reference to the GameManager script

    void Start() {

    }
    
    void Update() {
        if (Camera.main != null) {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            if (active == true && distance <= showHighlightDistance) {
                highlight.SetActive(true);
            } else {
                highlight.SetActive(false);
            }
        }
    }

    // when the player is within 80 units of the minigame entry and the object is clicked, the minigame will start
    public void OnMouseDown()
    {
        if (active == true && distance <= interactionDistance)
        {
            // Use the GameManager reference to load the minigame scene
            if (gameManager != null)
            {
                int minigameIndex = gameManager.GetMinigameIndex(sceneToLoad);
                if (minigameIndex >= 0)
                {
                    gameManager.LoadMinigame(minigameIndex);
                }
                gameManager.deactivateMiniGame(minigameIndex);
            }
        }
    }


    public void activate(bool b) {
        active = b;
        UIHighlight.SetActive(b);
    }
    
    public string getName() {
        return name;
    }

    public bool isActive() {
        return active;
    }
}
 