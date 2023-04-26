using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMinigameManager : MonoBehaviour
{
    public Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
    private int[] indices = new int[] { 0, 1, 2, 3 };
    private SpriteRenderer[] spriteRenderers = new SpriteRenderer[4];
    private int correctWire;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            spriteRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        for (int i = 0; i < 4; i++)
        {
            spriteRenderers[i].color = colors[indices[i]];
        }

        indices[0] = (indices[0] + 1) % colors.Length;
        if (indices[0] == 0)
        {
            for (int i = 1; i < 4; i++)
            {
                indices[i] = (indices[i] + 1) % colors.Length;
            }
        }
    }

}
