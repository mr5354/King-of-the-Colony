using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSystem : MonoBehaviour
{
    public Color[] availableColors;

    void Start()
    {
        // Shuffle the array of available colors
        Shuffle(availableColors);

        // Assign a different random color to each child object
        for (int i = 0; i < transform.childCount; i++)
        {
            // Make sure we don't assign the same color twice
            int index = Random.Range(0, availableColors.Length);
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = availableColors[index];
            // Remove the assigned color from the array of available colors
            List<Color> tempList = new List<Color>(availableColors);
            tempList.RemoveAt(index);
            availableColors = tempList.ToArray();
        }
    }

    // Helper function to shuffle an array
    private void Shuffle<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }
    }
}
