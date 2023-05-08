using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireChew : MonoBehaviour
{

    private GameObject ChewEffect;

    public void OnMouseDown() {
        gameObject.SetActive(false);
        Debug.Log("hidden");
    }
}
