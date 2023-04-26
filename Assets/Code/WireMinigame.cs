using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WireMinigame : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    public void OnMouseDown() {
        Destroy(gameObject);
    }
}
