using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    public GameObject spritePrefab; // the sprite prefab to spawn
    public GameObject boundaryObject; // the game object to use as the boundary for spawning sprites
    
    public float spawnIntervalSeconds; // the time interval in seconds between sprite spawns

    private float spawnInterval; // the time interval between sprite spawns
    private float timer; // the timer for spawning sprites
    private float minX; // the minimum X position where sprites can be spawned
    private float maxX; // the maximum X position where sprites can be spawned
    private float minY; // the minimum Y position where sprites can be spawned
    private float maxY; // the maximum Y position where sprites can be spawned
    private float spriteWidth; // the width of the sprite prefab
    private float spriteHeight; // the height of the sprite prefab

    private bool isSpawning = true; // Flag to control whether squares should be spawned
    private List<GameObject> spawnedSquares = new List<GameObject>(); // List to keep track of spawned squares

    void Start()
    {
        // get the dimensions of the boundary object
        float objectWidth = boundaryObject.transform.localScale.x;
        float objectHeight = boundaryObject.transform.localScale.y;

        // get the dimensions of the sprite prefab
        spriteWidth = spritePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHeight = spritePrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        // calculate the boundaries for spawning sprites inside the object
        minX = boundaryObject.transform.position.x - objectWidth / 2f + spriteWidth / 2f;
        maxX = boundaryObject.transform.position.x + objectWidth / 2f - spriteWidth / 2f;
        minY = boundaryObject.transform.position.y - objectHeight / 2f + spriteHeight / 2f;
        maxY = boundaryObject.transform.position.y + objectHeight / 2f - spriteHeight / 2f;

        // set the spawn interval based on the specified time unit
        spawnInterval = spawnIntervalSeconds;
    }

    void Update()
    {
        // Only spawn squares if isSpawning is true
        if (isSpawning)
        {
            // increment the timer
            timer += Time.deltaTime;

            // check if it's time to spawn a new sprite
            if (timer >= spawnInterval)
            {
                // reset the timer
                timer = 0f;

                // generate a random position within the object boundary
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);
                Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

                // instantiate a new sprite prefab at the random position
                GameObject newSquare = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);
                spawnedSquares.Add(newSquare); // Add the new square to the list
            }
        }
    }

        // Public method to toggle spawning
    public void ToggleSpawning(bool shouldSpawn)
    {
        isSpawning = shouldSpawn;
    }

    // Public method to destroy all spawned squares
    public void DestroyAllSquares()
    {
        foreach (GameObject square in spawnedSquares)
        {
            Destroy(square);
        }
        spawnedSquares.Clear();
    }
}

