using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator anim;
    private Rigidbody2D rb;
    Vector2 movement;

    public HappinessBar bar;
    public int maxHappiness = 100;
    public int currentHappiness;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHappiness = 0;
        bar.SetMaxHappiness(maxHappiness);
    }

    void Update()
    {
        // Player Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Horizontal" , movement.x);
        anim.SetFloat("Vertical" , movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // public void HappinessBarController()
    // {
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         currentHappiness += 10;
    //         bar.SetHappiness(currentHappiness);        
    //     }
    // }
}
