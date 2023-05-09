using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FakeNewsManager : MonoBehaviour
{
    // questions and answers
    private List<string[]> questions;
    private List<bool[]> answers;
    // num of rounds
    private int totalRounds = 10;
    private int currRound = 0;

    // question set
    private int currSetIdx;

    // choice objects
    public GameObject choice1;
    public GameObject choice2;
    public GameObject choice3;
    // choice text
    public TextMeshProUGUI choiceText1;
    public TextMeshProUGUI choiceText2;
    public TextMeshProUGUI choiceText3;
    private TextMeshProUGUI[] choiceTextArray;
    // score
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int scoreMultiplier = 1;

    public string additiveSceneName;
    public GameObject FinalCanvas;
    public TextMeshProUGUI finalScoreText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the FinalCanvas
        FinalCanvas.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        // choice text array
        choiceTextArray = new TextMeshProUGUI[] {choiceText1, choiceText2, choiceText3};
        // init questions
        questions = new List<string[]>
        {
            new string[] {"Sanitation workers are coming to cleaning these areas... Be aware!", "Be aware of toxic gases in the following areas...", "Human love rats!"},
            new string[] {"Don't go to these houses, they got cats in 'em.", "We can survive a fall from a great height without injury. Time to go bungee jumping!", "Go to this restaurant, they have the best waste food!"},
            new string[] {"Cheese is safe to eat in the kitchen.", "There will be less time for us to dig through dumpsters now since the trash setout rule is changing.", "There are bait stations on the streets. Be careful"},
            new string[] {"Rats can hold their breath for over an hour under water! You can hide from humans like that.", "There's a new rat poison in the neighborhood, be careful!", "The sewer system is a safe place to hide."},
            new string[] {"Rats can survive without water for more than a few days.", "The humans are planning a rat extermination in the area.", "Rats can eat anything without getting sick."},
            new string[] {"Rats have excellent eyesight, so we don't rely on our other senses.", "The humans have left out food for us, it's a feast!", "Rats have an incredible sense of smell. We can use it to avoid traps."},
            new string[] {"Rats can breed slowly, with females able to have only one litter a year.", "The humans have left out traps, watch your step!", "Rats are solitary creatures and prefer to live alone."},
            new string[] {"Rats are diurnal, so it's safer to go out during the night.", "The humans are moving out, so there will be less food.", "Be careful of the humans' pet cats."},
            new string[] {"Rats can't squeeze through holes as small as a quarter.", "The humans have a new pet snake, be careful!", "Rats can't survive in cold weather."},
            new string[] {"Rats can't climb vertical surfaces.", "The humans are using ultrasonic devices to drive us away.", "Rats can't remember their way back home."}

        };
        // init answers
        answers = new List<bool[]>
        {
            new bool[] {false, false, true},
            new bool[] {false, true, false},
            new bool[] {true, false, false},
            new bool[] {true, false, false},
            new bool[] {true, false, false},
            new bool[] {true, false, false},
            new bool[] {true, false, false},
            new bool[] {true, false, false},
            new bool[] {true, false, false},
            new bool[] {true, false, false}

        };
        SelectQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        if (currRound != totalRounds)
        {
            // if the player clicks on a choice object
            if (Input.GetMouseButtonDown(0))
            {
                // get the mouse position
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // get the collider of the object
                Collider2D collider = Physics2D.OverlapPoint(mousePos);
                // if the collider is not null
                if (collider != null)
                {
                    // get the game object
                    GameObject obj = collider.gameObject;
                    if (obj == choice1 || obj == choice2 || obj == choice3)
                    {
                        if (isCorrect(obj))
                        {
                            // increment score
                            score++;
                            // update score text
                            scoreText.text = "Score: " + score * scoreMultiplier;
                        }

                        questions.RemoveAt(currSetIdx);
                        answers.RemoveAt(currSetIdx);
                        currRound++;
                        // select another set of questions
                        if (currRound != totalRounds) {
                            SelectQuestions();
                        }
                    }
                }
            }
        }
        else
        {
            // end of game
            EndGame();
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
                {
                    // Unload the scene after a mouse click or key press
                    UnloadCurrentScene();
                    gameManager.UpdateHappiness(score * scoreMultiplier);
                }
        }
    }

    // randomly select another set of questions
    private void SelectQuestions()
    {
        // randomly select 3 questions
        currSetIdx = Random.Range(0, questions.Count - 1);
        string[] currSet = questions[currSetIdx];
        // set the text of the choices
        for (int i = 0; i < currSet.Length; i++)
        {
            // get the question
            string question = currSet[i];
            choiceTextArray[i].text = question;
        }
    }

    private bool isCorrect(GameObject obj) {
        Debug.Log("Current round is: " + currRound);
        Debug.Log("isCorrect is called");
        if (obj == choice1) {
            return answers[currSetIdx][0];
        } else if (obj == choice2) {
            return answers[currSetIdx][1];
        } else if (obj == choice3) {
            return answers[currSetIdx][2];
        } else {
            return false;
        }
    }

    private void EndGame()
    {
        // Add any additional end-of-game functionality here
        FinalCanvas.SetActive(true);
        // Set the final score text 
        finalScoreText.text = "You were able to spread " + score + "/10 fake news! \n Your score was: " + score * scoreMultiplier;

    }

    // function to handle end of game
    private void UnloadCurrentScene()
    {
        if (!string.IsNullOrEmpty(additiveSceneName))
        {
            // Unload the current additive scene
            SceneManager.UnloadSceneAsync(additiveSceneName);
            additiveSceneName = null;

            // Call the ReactivatePlayerAndUI function from the GameManager script
            if (gameManager != null)
            {
                gameManager.ReactivatePlayerAndUI();
            }
        }
    }

}
