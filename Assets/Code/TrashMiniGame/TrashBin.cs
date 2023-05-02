using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        // check if the other object has the "TrashSprite" tag
        if (other.CompareTag("TrashSprite"))
        {
            // destroy the other object
            Destroy(other.gameObject);
        }
    }

    // void OnTriggerStay2D(Collider2D other) {
    //     // this method is called every frame while the other object is inside the trigger
    // }

    // void OnTriggerExit2D(Collider2D other) {
    //     // this method is called when the other object exits the trigger
    // }
}
