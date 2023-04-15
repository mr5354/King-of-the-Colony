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
    public static GameObject chewWireGame;
    public static GameObject garbageCollectionGame;
    public static GameObject fakeInfoGame;
    public static GameObject minigame4;
    public static GameObject minigame5;
    
    private static GameObject[] minigames = {chewWireGame, garbageCollectionGame, fakeInfoGame, minigame4, minigame5};

    private static bool activated = false;

    void Start()
    {
        // Find the HappinessBar object in the scene and get a reference to it
        bar = FindObjectOfType<HappinessBar>();
        
        // Set the happiness to 100
        bar.SetMaxHappiness(happiness);

        TimerOn = true;

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

            // Debug.Log("can i activate: " + !activated);

            if ((int)countdownTime % 10 == 0 && countdownTime > 0 && !activated) {
                MinigameActivator();
                activated = true;
            } else if ((int)countdownTime % 10 != 0) {
                activated = false;
            }
        }
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
                count++;
            }
        }
        Debug.Log("# of minigame activated: " + count);
        // if no minigames are activated, activate at least one
        if (count == 0) {
            minigameArray[Random.Range(0, 5)] = true;
            // activate this minigame
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