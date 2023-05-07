using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WireMiniGameManager : MonoBehaviour
{
    public GameObject[] wires;
    public GameObject[] indicators;

    private int correctWireIndex;
    private int score = 0;

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        ShuffleWires();
    }

    private void ShuffleWires()
    {
        // Shuffle the wires array
        for (int i = 0; i < wires.Length; i++)
        {
            GameObject temp = wires[i];
            int randomIndex = Random.Range(i, wires.Length);
            wires[i] = wires[randomIndex];
            wires[randomIndex] = temp;
        }

        // Set the wire colors
        List<Color> availableColors = new List<Color>{Color.red, Color.green, Color.blue};
        for (int i = 0; i < wires.Length; i++)
        {
            // Choose a random color
            int randomColorIndex = Random.Range(0, availableColors.Count);
            Color randomColor = availableColors[randomColorIndex];
            // Set the wire color
            wires[i].GetComponent<Renderer>().material.color = randomColor;
            // Remove the color from the available colors
            availableColors.RemoveAt(randomColorIndex);
        }

        // Choose a random wire to be the correct one
        correctWireIndex = Random.Range(0, wires.Length);

        // Set the indicator colors to match the correct wire
        for (int i = 0; i < indicators.Length; i++)
        {
            if (i == correctWireIndex)
            {
                indicators[i].GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                indicators[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }


    public void WireClicked(GameObject wire)
    {
        if (wire == wires[correctWireIndex])
        {
            // Correct wire was clicked
            score++;
            scoreText.text = "Score: " + score.ToString();
            ShuffleWires();
        }
        else
        {
            // Incorrect wire was clicked
            ShuffleWires();
        }
    }
}
