using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Spawner : MonoBehaviour
{
    public GameObject cheese;
    public float objectCooldown;
    float nextObject;
    public int force = 600 ;
    Vector3 EndPos = new Vector2(10f, 0f);

    public static bool isSpawning = true; // Flag to control whether squares should be spawned

    // Start is called before the first frame update
    void Start()
    {
        nextObject = Time.time + objectCooldown;
        float randomCDMod = Random.Range(-1.0f, 2.0f);
        objectCooldown += randomCDMod;
        nextObject = Time.time + objectCooldown;
        objectCooldown -= randomCDMod;
        
    }

    // Update is called once per frames
    void FixedUpdate()
    {
        if(isSpawning){
            int value = Random.Range(1, 3);
            //int score = int.Parse(scoreLabel.text);
            //int force = score / 5;
            if(Time.time > nextObject && value == 1)
            {
                GameObject newCheese = Instantiate(cheese, transform.position, Quaternion.identity);
                newCheese.GetComponent<Rigidbody2D>().AddForce(new Vector2(force * transform.localScale.x, 0));
                float randomCDMod = Random.Range(-0.8f, .6f);
                objectCooldown += randomCDMod;
                nextObject = Time.time + objectCooldown;
                //Debug.Log(objectCooldown.ToString());
                objectCooldown -= randomCDMod;
                Destroy(newCheese, 2);
            }   
        }
        
    }

    public static void ToggleSpawning(bool shouldSpawn)
    {
        isSpawning = shouldSpawn;
    }
}
