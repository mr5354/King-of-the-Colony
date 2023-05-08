using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWalls : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }
    }
}
