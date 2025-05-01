using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    /*
        To set up a interactable create a isTrigger collider2d on the object.
        Add a second collider to make sure the player can't walk through the object
        Then add a rigidbody and make sure to constrain it to make sure it doesn't move.  Only required if you dont want to walk through it.  
        Add a tag to the object if needed
     */


    public float playerspeed = 5f;
    bool inTriggerNPC = false;
    bool inTriggerPickUp = false;

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
        //Records the input of the player using the Unity Default Horizontal Axis.  Allows for both controller and keyboard input.
        float horizontalInput = Input.GetAxis("Horizontal");

        //Moves the player using transform translate which allows for movement in both directions.  Right is the positive direction because of Vector3.right.
        transform.Translate(Vector3.right * playerspeed * horizontalInput * Time.deltaTime);

        //Records the input of the player using the Unity Default Vertical Axis.  Allows for both controller and keyboard input.
        float verticalInput = Input.GetAxis("Vertical");

        //Moves the player using transform translate which allows for movement in both directions.  Up is the positive direction because of Vector3.up
        transform.Translate(Vector3.up * playerspeed * verticalInput * Time.deltaTime);

        //Checks if the player is inside a trigger box and if they pressed e.  
        if (Input.GetKeyDown(KeyCode.E) && inTriggerNPC)
        {
            //Place the UI activation for NPCs here
            Debug.Log("UI Dialouge Box Appears Now");
        } else if (Input.GetKeyDown(KeyCode.E) && inTriggerPickUp)
        {
            //Place the UI activation and inventory addition for Items here
            //Debug.Log("UI Pickup and Inventory Addition");
            if(currPickUp != null)
            {
                currPickUp.GetComponent<PickUp>().addToInventory();
            }

        }

        /*float up = 0.0f;
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


        transform.position = new Vector3(transform.position.x + (right - left) * movementSpeed,transform.position.y + (up - down) * movementSpeed,0.0f);*/

    }

    private GameObject currPickUp = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //call a compare tag on the collison then set the appropriate bool to true.
        if (collision.tag == "NPC") 
        {
            inTriggerNPC = true;
        }
        if (collision.tag == "PickUp")
        {
            inTriggerPickUp = true;
            currPickUp = collision.gameObject;
        }
        if (collision.tag == "PatrolEnemy") 
        {
            navPatrol navPatrol = collision.gameObject.GetComponent<navPatrol>();
            navPatrol.foundPlayer();
        }
        if (collision.tag == "sceneChanger") 
        {
            sceneChanger sceneChanger = collision.gameObject.GetComponent<sceneChanger>();
            sceneChanger.changeScene();
        }

        if (collision.tag == "Enemy" || collision.tag == "Projectile") 
        {
            Debug.Log("Kill/Damage Player" + collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //call a compare tag on the collison then set the appropriate bool to false.  
        if (collision.tag == "NPC")
        {
            inTriggerNPC = false;
        }
        if (collision.tag == "PickUp")
        {
            inTriggerPickUp = false;
            currPickUp = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Projectile" || collision.collider.tag == "PatrolEnemy") 
        {
            Debug.Log("Kill/Damage Player" + collision.collider.name);
        }
    }

}
