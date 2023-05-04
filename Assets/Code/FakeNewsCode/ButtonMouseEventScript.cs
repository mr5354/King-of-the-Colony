using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMouseEventScript : MonoBehaviour
{    
    private PublishButton otherScript;
    
    private void Start()
    {
        otherScript = GetComponent<PublishButton>();
        // Debug.Log if the script is found
        if(otherScript == null) {
            Debug.Log("There is no publish script found on this object!!!");
        }
    }

    private void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // If the mouse was clicked, call the OnPointerDown function of the PublishButton script
            if (otherScript != null)
            {
                otherScript.OnPointerDown();
            }
        }

        // Check if the left mouse button was released
        if (Input.GetMouseButtonUp(0))
        {
            // If the mouse was released, call the OnPointerUp function of the PublishButton script
            if (otherScript != null)
            {
                otherScript.OnPointerUp();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Debug Log to check if the object has entered the trigger
        otherScript.OnPointerEnter();
    }

    void OnTriggerExit2D(Collider2D other) {
        // Debug Log to check if the object has exited the trigger
        Debug.Log("Object exited the trigger");
        if (otherScript != null)
        {
            otherScript.OnPointerExit();
        }
    }
}
