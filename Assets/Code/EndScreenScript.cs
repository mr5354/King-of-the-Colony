using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndScreenScript : MonoBehaviour
{
    private GameManager gameManager;
    private int bestScore = 100;
    private int currScore;

    public TextMeshProUGUI scoreTextObj;
    public TextMeshProUGUI bestScoreTextObj;
    public TextMeshProUGUI endingParagraphObj;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        

        if (gameManager != null)
        {
            currScore = gameManager.getHappiness();
            if (currScore < bestScore)
            {
                bestScore = gameManager.getHappiness();
            }
            bestScoreTextObj.text = "Lowest happiness was: " + bestScore;
            scoreTextObj.text = "The remaining happiness of the rats colony is: " + currScore;
            // if the happiness is above 80, the player lose
            if (currScore > 80)
            {
                // display lose text
                endingParagraphObj.text = "Alas, dear player, your dastardly schemes have been foiled! The rats, those cunning whiskered masterminds, have outwitted you at every turn. Despite your best efforts to sabotage their colony, they've emerged triumphant, tails held high. You've lost the game, but take heart, for in the epic battle of human versus rodent, there's always a next time for sneaky shenanigans!";
            }
            else if (currScore < 40)
            {
                // display win text
                endingParagraphObj.text = "Congratulations, master saboteur! Your cunning schemes have thrown the rat colony into utter chaos. The rats are scurrying in confusion, and their plans for world domination are doomed! You've saved the day, and your name will go down in history as the hero who outwitted the rodents!";
            } else {
                // display neutral text
                endingParagraphObj.text = "Well, that was... uneventful. You dabbled in the art of sabotage, but the rat colony remains mostly unfazed. They're neither triumphant nor defeated, just carrying on with their ratty business. Perhaps next time, you'll tip the scales one way or the other.";
            }
        } else {
            scoreTextObj.text = "The remaining happiness of the rats colony is: 100";
            bestScoreTextObj.text = "Lowest happiness was: 50";
            endingParagraphObj.text = "Alas, dear player, your dastardly schemes have been foiled! The rats, those cunning whiskered masterminds, have outwitted you at every turn. Despite your best efforts to sabotage their colony, they've emerged triumphant, tails held high. You've lost the game, but take heart, for in the epic battle of human versus rodent, there's always a next time for sneaky shenanigans!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
