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
    // public AudioSource click;

    void Update() {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (active == true && distance <= showHighlightDistance) {
            highlight.SetActive(true);
        } else {
            highlight.SetActive(false);
        }
    }

    // when the player is within 80 units of the minigame entry and the object is clicked, the minigame will start
    public void OnMouseDown() {
        // click.Play();
        Debug.Log("Distance is: " + distance);
        Debug.Log("Interaction Distance is: " + interactionDistance);
        if (active == true && distance <= interactionDistance) {
           // SceneManager.LoadScene(sceneToLoad);
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            Player.SetActive(false);
            // hide the UI
            GameObject.Find("Canvas").SetActive(false);
            // pause the timer in game manager
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
