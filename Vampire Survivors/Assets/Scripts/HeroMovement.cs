using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    //-------------------------------------------------------------

    public float speed;
    Rigidbody2D rb2D;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalV;
    [HideInInspector]
    public float lastVerticalV;

    //-------------------------------------------------------------

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        
    }
    //-------------------------------------------------------------

    void Update()
    {
        InputManager();
    }

    //-------------------------------------------------------------

    void FixedUpdate() //instead of Update because its not frame depended.
    {
        Mover();
    }
    //-------------------------------------------------------------

    void InputManager()
    {
        float Xmover = Input.GetAxisRaw("Horizontal");
        float YMover = Input.GetAxisRaw("Vertical"); 
        moveDir = new Vector2(Xmover, YMover).normalized;

        if (moveDir.y != 0)
        {
            lastVerticalV = moveDir.y;
        }
        if (moveDir.x != 0)
        {
            lastHorizontalV = moveDir.x;
        }    
    }

    //-------------------------------------------------------------

    void Mover()
    {
        rb2D.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
    }
}
