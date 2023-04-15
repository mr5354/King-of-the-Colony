using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int happiness = 100;
    private HappinessBar bar;
    private float countdownTime = 5f * 60f; // 5 minutes in seconds
    private bool TimerOn = false;
    public TextMeshProUGUI timerText;
    public string sceneToLoad;
    
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