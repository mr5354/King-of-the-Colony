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
    private int totalRounds = 3;
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
    private bool isGameOver = false;

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
            new string[] {"q1", "q2", "q3"},
            new string[] {"q4", "q5", "q6"},
            new string[] {"q7", "q8", "q9"}
        };
        // init answers
        answers = new List<bool[]>
        {
            new bool[] {false, false, true},
            new bool[] {false, true, false},
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
                    // if the object is a choice object
                    if (obj == choice1 || obj == choice2 || obj == choice3)
                    {
                        if (isCorrect(obj))
                        {
                            answers.RemoveAt(currSetIdx);
                            // increment score
                            score++;
                            // update score text
                            scoreText.text = "Score: " + score * scoreMultiplier;
                        }
                        currRound++;
                        // select another set of questions
                        if (currRound != totalRounds) {
                            SelectQuestions();
                        }
                    }
                }
            }
        }
        else if (!isGameOver)
        {
            // end of game
            EndGame();
            isGameOver = true;
        }
        else if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            // Unload the scene after a mouse click or key press
            UnloadCurrentScene();
            gameManager.UpdateHappiness(score * scoreMultiplier);
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

        // remove the selected set from the questions and answers lists
        questions.RemoveAt(currSetIdx);
    }

    private bool isCorrect(GameObject obj) {
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
        finalScoreText.text = "Final Score: " + score * scoreMultiplier;
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
