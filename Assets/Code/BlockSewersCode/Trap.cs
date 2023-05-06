using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite activeTrap;
    public Sprite unactiveTrap;
    private GameManager gameManager;

    public int getActivatedTraps;
    //public Sprite unactiveTrap;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // if trap is not clicked on, activate it
    void OnMouseDown(){
        spriteRenderer.sprite = activeTrap;
        PublicVars.activeTraps ++;
        
    }

    // activate some traps when minigame starts
    public void Activate(bool active) {
        if(active){
            spriteRenderer.sprite = activeTrap;
        }
    }
    
}
