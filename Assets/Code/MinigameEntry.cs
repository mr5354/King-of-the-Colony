using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamEntry : MonoBehaviour
{
    public float interactionDistance;
    private float showHighlightDistance = 11f;
    public string sceneToLoad;
    public GameObject highlight;
    private float distance;
    private static bool active = false;

    void Update() {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (distance <= showHighlightDistance && active) {
            highlight.SetActive(true);
        } else {
            highlight.SetActive(false);
        }
    }

    // when the player is within 80 units of the minigame entry and the object is clicked, the minigame will start
    public void OnMouseDown() {
        Debug.Log("Distance is: " + distance);
        Debug.Log("Interaction Distance is: " + interactionDistance);
        if (active && distance <= interactionDistance) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void activate(bool b) {
        active = b;
        Debug.Log("Minigame activated");
    }

}
