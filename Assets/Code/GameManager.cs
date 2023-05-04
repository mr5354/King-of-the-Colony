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
    // private float countdownTime = 20f + 1f; // 20second - for debugging
    private float countdownTime = 5f * 60f + 1f; // 5 minutes in seconds
    private bool TimerOn = false;
    public TextMeshProUGUI timerText;
    public string sceneToLoad;
    private string currentMinigameScene;
    // private int toSkip = null;
    public GameObject canvas;

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
        // ==============Debug helpers==============

        // LoadMinigameWithDebugKeys();
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     // Unload the current minigame scene if one is loaded
        //     UnloadCurrentMinigame();
        // }

        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     // Decrement happiness by 5
        //     UpdateHappiness(5);
        // }

        // ==============Debug helpers end==============

        if (happiness == 0) {
            TimerOn = false;
            SceneManager.LoadScene(sceneToLoad);
            activated = false;
        }

        if (TimerOn)
        {
            countdownTime -= Time.deltaTime;
            timerText.text = FormatTime(countdownTime);
            if (countdownTime <= 0f)
            {
                TimerOn = false;
                SceneManager.LoadScene(sceneToLoad);
                activated = false;
            }

            if ((countdownTime == 5f * 60f) || ((int)countdownTime % 10 == 0 && countdownTime > 0 && !activated)) {
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
                count++;
            } else {
                minigames[i].activate(false);
            }
        }
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
        if (minutes >= 0 && seconds >= 0) {
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        } else {
            return "00:00";
        }
    }

    // Whenever a minigame ends, the happiness will be updated
    public void UpdateHappiness(int val)
    {
        // Debug log when this is called
        happiness -= val;
        // Clamp the happiness value to ensure it doesn't go below 0
        happiness = Mathf.Max(0, happiness);
        // Update the health bar to reflect the new happiness value
        bar.SetHappiness(happiness);
    }


    private void LoadMinigameWithDebugKeys()
    {
        // Check if the number keys 1-5 are pressed
        for (int i = 0; i < minigames.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // Load the corresponding minigame
                LoadMinigame(i);
            }
        }
    }

    public void LoadMinigame(int index)
    {
        if (index >= 0 && index < minigames.Length)
        {
            // Unload the current minigame scene if one is loaded
            UnloadCurrentMinigame();

            // Load the specified minigame additively
            currentMinigameScene = minigames[index].sceneToLoad;
            SceneManager.LoadScene(currentMinigameScene, LoadSceneMode.Additive);

            // Deactivate the player (if needed)
            minigames[index].Player.SetActive(false);
            // Hide the UI (if needed)
            // GameObject.Find("Canvas").SetActive(false);
            canvas.SetActive(false);
            // Pause the timer in the game manager (if needed)
            TimerOn = false;
        }
    }
    
    private void UnloadCurrentMinigame()
    {
        // Unload the current minigame scene if one is loaded
        if (!string.IsNullOrEmpty(currentMinigameScene))
        {
            // Check if the scene is loaded before attempting to unload it
            Scene sceneToUnload = SceneManager.GetSceneByName(currentMinigameScene);
            if (sceneToUnload.isLoaded)
            {
                SceneManager.UnloadSceneAsync(currentMinigameScene);
            }
            currentMinigameScene = null;

            // Reactivate the player (if needed)
            minigames[0].Player.SetActive(true); // Assuming all minigames use the same player reference
            // Show the UI (if needed)
            // GameObject.Find("Canvas").SetActive(true);
            canvas.SetActive(true);
            // Resume the timer in the game manager (if needed)
            TimerOn = true;
        }
    }


    public void ReactivatePlayerAndUI()
    {
        // Reactivate the player (if needed)
        minigames[0].Player.SetActive(true); // Assuming all minigames use the same player reference
        // Show the UI (if needed)
        canvas.SetActive(true);
        // Resume the timer in the game manager (if needed)
        TimerOn = true;
    }

    public int GetMinigameIndex(string sceneName)
    {
        for (int i = 0; i < minigames.Length; i++)
        {
            if (minigames[i].sceneToLoad == sceneName)
            {
                return i;
            }
        }
        return -1; // Return -1 if the scene name is not found
    }

    public int getHappiness() {
        return happiness;
    }

    public void deactivateMiniGame(int idx) {
        minigames[idx].activate(false);
    }
}