using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawAnimations : MonoBehaviour
{
    private Animator anim;
    private int lastDirection = 1;

    private Rigidbody2D rb;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private Vector3 previousPosition;
    private Vector3 currentMovementDirection;

    void Update()
    {
        if (previousPosition != transform.position)
        {
            currentMovementDirection = (previousPosition - transform.position).normalized;
            previousPosition = transform.position;
        }

        StartMoving();
    }

    void StartMoving()
    {
        if (currentMovementDirection.y < 0 && Mathf.Abs(currentMovementDirection.y) > Mathf.Abs(currentMovementDirection.x))
        {
            anim.Play("MawBackwardsWalk");
            lastDirection = 0;
        }
        else if (currentMovementDirection.y > 0 && Mathf.Abs(currentMovementDirection.y) > Mathf.Abs(currentMovementDirection.x))
        {
            anim.Play("MawForwardsWalk");
            lastDirection = 1;
        }
        else if (currentMovementDirection.x < 0)
        {
            anim.Play("MawRightWalk");
            lastDirection = 2;
        }
        else if (currentMovementDirection.x > 0)
        {
            anim.Play("MawLeftWalk");
            lastDirection = 3;
        }
        else if (currentMovementDirection.y == 0 && currentMovementDirection.x == 0)
        {
            StopMoving();
        }
    }

    void StopMoving()
    {
        if (lastDirection == 0)
        {
            anim.Play("MawIdleBackwards");
        }
        else if (lastDirection == 1)
        {
            anim.Play("MawIdleForwards");
        }
        else if (lastDirection == 2)
        {
            anim.Play("MawIdleRight");
        }
        else if (lastDirection == 3)
        {
            anim.Play("MawIdleLeft");
        }
    }
}
