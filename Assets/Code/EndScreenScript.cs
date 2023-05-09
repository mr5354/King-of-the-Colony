using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenScript : MonoBehaviour
{
    private GameManager gameManager;
    private BestScore bestScoreManager;

    public TextMeshProUGUI scoreTextObj;
    public TextMeshProUGUI bestScoreTextObj;
    public TextMeshProUGUI endingParagraphObj;

    void Start()
    {
        // Find the GameManager and BestScore instances
        gameManager = FindObjectOfType<GameManager>();
        bestScoreManager = BestScore.Instance;

        // If the GameManager exists, calculate the score and update the UI
        if (gameManager != null)
        {
            // Calculate the current score as 100 minus the happiness
            int currScore = 100 - gameManager.getHappiness();

            // If the current score is higher than the best score, update the best score
            if (currScore > bestScoreManager.GetScore())
            {
                bestScoreManager.SetScore(currScore);
            }

            // Update the score and best score text
            scoreTextObj.text = "You scored: " + currScore;
            bestScoreTextObj.text = "Best score was: " + bestScoreManager.GetScore();

            // Determine the ending text based on the current score
            if (currScore < 20)
            {
                endingParagraphObj.text = "Alas, dear player, your dastardly schemes have been foiled! The rats, those cunning whiskered masterminds, have outwitted you at every turn. Despite your best efforts to sabotage their colony, they've emerged triumphant, tails held high. You've lost the game, but take heart, for in the epic battle of human versus rodent, there's always a next time for sneaky shenanigans!";
            }
            else if (currScore > 60)
            {
                endingParagraphObj.text = "Congratulations, master saboteur! Your cunning schemes have thrown the rat colony into utter chaos. The rats are scurrying in confusion, and their plans for world domination are doomed! You've saved the day, and your name will go down in history as the hero who outwitted the rodents!";
            }
            else
            {
                endingParagraphObj.text = "Well, that was... uneventful. You dabbled in the art of sabotage, but the rat colony remains mostly unfazed. They're neither triumphant nor defeated, just carrying on with their ratty business. Perhaps next time, you'll tip the scales one way or the other.";
            }
        }
        else
        {
            // If the GameManager doesn't exist, display default text
            scoreTextObj.text = "The remaining happiness of the rats colony is: 100";
            bestScoreTextObj.text = "Lowest happiness was: 50";
            endingParagraphObj.text = "Alas, dear player, your dastardly schemes have been foiled! The rats, those cunning whiskered masterminds, have outwitted you at every turn. Despite your best efforts to sabotage their colony, they've emerged triumphant, tails held high. You've lost the game, but take heart, for in the epic battle of human versus rodent, there's always a next time for sneaky shenanigans!";
        }
    }
}
