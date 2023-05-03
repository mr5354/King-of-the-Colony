using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart(string LevelName)
    {

        Debug.Log("Restarting game");
        SceneManager.LoadScene(LevelName);
        // destroy the game manager
        Destroy(FindObjectOfType<GameManager>().gameObject);
        
    }
}
