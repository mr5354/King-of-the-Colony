using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusHightlight : MonoBehaviour
{
    public MinigameEntry minigame;
    

    // Update is called once per frame
    void Update()
    {
        if (minigame.isActive() == true) {
            Debug.Log("UI STATUS is active: " + minigame.getName());
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
