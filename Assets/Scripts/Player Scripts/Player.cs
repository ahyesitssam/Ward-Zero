using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public float movementSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {

        float up = 0.0f;
        float down = 0.0f;
        float right = 0.0f;
        float left = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            up = 1.0f;
        }
        else
        {
            up = 0.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            down = 1.0f;
        }
        else
        {
            down = 0.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            left = 1.0f;
        }
        else
        {
            left = 0.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            right = 1.0f;
        }
        else
        {
            right = 0.0f;
        }


        transform.position = new Vector3(transform.position.x + (right - left) * movementSpeed,transform.position.y + (up - down) * movementSpeed,0.0f);

    }

}
