using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrashSprite : MonoBehaviour
{
    public Sprite[] sprites; // Array of sprites to choose from

    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the sprites array is not empty
        if (sprites != null && sprites.Length > 0)
        {
            // Select a random index from the array of sprites
            int randomIndex = Random.Range(0, sprites.Length);

            // Assign the randomly selected sprite to the SpriteRenderer
            spriteRenderer.sprite = sprites[randomIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}