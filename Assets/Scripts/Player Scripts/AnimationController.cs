using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;
    private int lastDirection = 0; //  0 ^, 1 \/, 2 <, 3 >

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    float horizontalInput;
    float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        StartMoving();
    }

    void StartMoving()
    {
        if (verticalInput > 0)
        {
            anim.Play("BackwardsWalk");
            lastDirection = 0;
        }
        else if (verticalInput < 0)
        {
            anim.Play("ForwardsWalk");
            lastDirection = 1;
        }
        else if (horizontalInput > 0)
        {
            anim.Play("RightWalk");
            lastDirection = 2;
        }
        else if (horizontalInput < 0)
        {
            anim.Play("LeftWalk");
            lastDirection = 3;
        }
        else if (horizontalInput == 0 && verticalInput == 0)
        {
            StopMoving();
        }
    }

    void StopMoving()
    {
        if (lastDirection == 0)
        {
            anim.Play("IdleBackwards");
        }
        else if (lastDirection == 1)
        {
            anim.Play("IdleForwards");
        }
        else if (lastDirection == 2)
        {
            anim.Play("IdleRight");
        }
        else if (lastDirection == 3)
        {
            anim.Play("IdleLeft");
        }
    }
}
