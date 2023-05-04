using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // mouse position
    private Vector3 mousePosition;
    private GameObject clickedObject; // the sprite currently being clicked

    public float xOffset = 0f; // x offset
    public float yOffset = 0f; // y offset

    void Update()
    {
        // get the mouse position in world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // make sure the Z coordinate is 0

        // update the game object's position to match the mouse position with offsets
        transform.position = new Vector3(mousePosition.x + xOffset, mousePosition.y + yOffset, mousePosition.z);

        // check if clickedObject has been destroyed
        if (clickedObject != null && clickedObject.GetComponent<TrashFollow>() == null)
        {
            // clear the reference to the destroyed object
            clickedObject = null;
        }

        // check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // cast a ray from the mouse position to detect sprites
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.CompareTag("TrashSprite") && clickedObject == null)
            {
                // get the object collided with
                clickedObject = hit.collider.gameObject;

                // call the setIsClicked function of the TrashFollow script
                clickedObject.GetComponent<TrashFollow>().setIsClicked(true);
            }
            else if (clickedObject != null)
            {
                // call the setIsClicked function of the previously clicked object to set it down
                clickedObject.GetComponent<TrashFollow>().setIsClicked(false);
                clickedObject = null;
            }
        }
    }
}





