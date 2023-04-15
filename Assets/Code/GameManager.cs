using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
// using Random;

public class GameManager : MonoBehaviour
{
    private int happiness = 100;
    private HappinessBar bar;
    private float countdownTime = 5f * 60f + 1f; // 5 minutes in seconds
    private bool TimerOn = false;
    public TextMeshProUGUI timerText;
    public string sceneToLoad;

    // minigames
    public MinigameEntry chewWireGame;
    public MinigameEntry garbageCollectionGame;
    public MinigameEntry fakeInfoGame;
    public MinigameEntry minigame4;
    public MinigameEntry minigame5;
    
    private MinigameEntry[] minigames;

    private static bool activated = false;

    // sound
    public AudioSource click;

    void Start()
    {
        // Find the HappinessBar object in the scene and get a reference to it
        bar = FindObjectOfType<HappinessBar>();
        
        // Set the happiness to 100
        bar.SetMaxHappiness(happiness);

        TimerOn = true;

        // minigames
        minigames = new MinigameEntry[] {chewWireGame, garbageCollectionGame, fakeInfoGame, minigame4, minigame5};

    }

    void Update()
    {
        if (TimerOn)
        {
            countdownTime -= Time.deltaTime;
            timerText.text = FormatTime(countdownTime);
            if (countdownTime <= 0f)
            {
                TimerOn = false;
                SceneManager.LoadScene(sceneToLoad);
            }

            if ((int)countdownTime % 10 == 0 && countdownTime > 0 && !activated) {
                MinigameActivator();
                activated = true;
            } else if ((int)countdownTime % 10 != 0) {
                activated = false;
            }
        }
        if(Input.GetMouseButtonDown(0)){
            click.Play();
        }
    }

    void Awake()
    {
        //Let the gameobject persist over the scenes
        DontDestroyOnLoad(gameObject);
    }

    // every minute, a random number of minigames will be activated. if none are activated, at least one will be
    void MinigameActivator() {
        // create an array of size 5
        bool[] minigameArray = new bool[5];
        int count = 0;
        for (int i = 0; i < minigameArray.Length; i++) {
            // geenrate random true or false value
            minigameArray[i] = Random.value < 0.5f;
            if (minigameArray[i]) {
                // activate minigame
                minigames[i].activate(true);
                Debug.Log("Minigame activated: " + minigames[i].getName());
                count++;
            } else {
                minigames[i].activate(false);
            }
        }
        Debug.Log("# of minigame activated: " + count);
        // if no minigames are activated, activate at least one
        if (count == 0) {
            int minigameToActivate = Random.Range(0, 5);
            minigames[minigameToActivate].activate(true);
        }

    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Whenever a minigame ends, the happiness will be updated
    public void UpdateHappiness(int val)
    {
        happiness -= val;
        bar.SetHappiness(happiness);
    }
}