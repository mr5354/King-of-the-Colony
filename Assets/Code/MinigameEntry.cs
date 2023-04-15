using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamEntry : MonoBehaviour
{
    public float interactionDistance;
    public string sceneToLoad;

    // when the player is within 80 units of the minigame entry and the object is clicked, the minigame will start
    public void OnMouseDown() {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Debug.Log("Distance is: " + distance);
        Debug.Log("Interaction Distance is: " + interactionDistance);
            if (distance <= interactionDistance) {
                SceneManager.LoadScene(sceneToLoad);
            }
    }
}
