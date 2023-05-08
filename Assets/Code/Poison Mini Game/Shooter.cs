using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adapted from https://www.youtube.com/watch?v=-bkmPm_Besk&t=619s
public class Shooter : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;

    public GameObject bullet;
    public Transform bulletTransform;

    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y , rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotz -90);

        if(!canFire){
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring){
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButton(0) && canFire){
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
