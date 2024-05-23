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
    [HideInInspector]
    public Vector2 lastMovedV;

    //-------------------------------------------------------------

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        lastMovedV = new Vector2(1, 0f);

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
            lastMovedV = new Vector2(0f, lastVerticalV);
        }
        if (moveDir.x != 0)
        {
            lastHorizontalV = moveDir.x;
            lastMovedV = new Vector2 (lastHorizontalV, 0);
        }    
        if(moveDir.y != 0 && moveDir.x != 0)
        {
            lastMovedV = new Vector2(lastHorizontalV, lastVerticalV);
        }
    }

    //-------------------------------------------------------------

    void Mover()
    {
        rb2D.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
    }
}
