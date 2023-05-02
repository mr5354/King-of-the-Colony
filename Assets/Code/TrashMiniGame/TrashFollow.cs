using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashFollow : MonoBehaviour
{
    private bool isClicked = false;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // make sure the Z coordinate is 0

            // update the game object's position to match the mouse position
            transform.position = mousePosition;
        }
    }

    public void setIsClicked(bool b)
    {
        isClicked = b;
    }
}