using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Newspaper : MonoBehaviour
{
    public GameObject popupPanel;
    public Image popupImage;
    private int score;

    private bool isFirstPopupDisplayed = false;
    private bool isSecondPopupDisplayed = false;
    private bool isThirdPopupDisplayed = false;

    private void Update()
    {  
        score = happiness;

        // Check if the player has reached the score threshold and the popup is not already displayed
        if (score <= 75 && !isFirstPopupDisplayed)
        {
            // Show the popup panel and set the image sprite
            popupImage.sprite = Resources.Load<Sprite>("PopupImage");
            popupPanel.SetActive(true);

            // Set the flag to prevent the popup from displaying multiple times
            isFirstPopupDisplayed = true;
        }
        else if (score <= 50 && !isSecondPopupDisplayed)
        {
            // Show the popup panel and set the image sprite
            popupImage.sprite = Resources.Load<Sprite>("PopupImage");
            popupPanel.SetActive(true);

            // Set the flag to prevent the popup from displaying multiple times
            isSecondPopupDisplayed = true;
        }
        else if (score <= 25 && !isThirdPopupDisplayed)
        {
            // Show the popup panel and set the image sprite
            popupImage.sprite = Resources.Load<Sprite>("PopupImage");
            popupPanel.SetActive(true);

            // Set the flag to prevent the popup from displaying multiple times
            isThirdPopupDisplayed = true;
        }
    }
}
