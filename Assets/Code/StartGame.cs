using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public void StartButton(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
}
