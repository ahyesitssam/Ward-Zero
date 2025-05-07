using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /*
        To set up a interactable create a isTrigger collider2d on the object.
        Add a second collider to make sure the player can't walk through the object
        Then add a rigidbody and make sure to constrain it to make sure it doesn't move.  Only required if you dont want to walk through it.  
        Add a tag to the object if needed
     */

    public float playerspeed = 5f;
    public bool canMove = true;
    bool inTriggerPickUp = false;
    private GameObject currPickUp = null;
    private DialogueManager DM;
    private InventoryMenu IM;
    private ActionTracker AT;
    private GameManager GM;


    [SerializeField] GameObject gertrudeTrigger;
    [SerializeField] GameObject haroldTrigger;
    [SerializeField] GameObject lillyTrigger;
    private int gertrudeIndex = 0;
    private int haroldIndex = 0;
    private int lillyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        DM = GameObject.Find("Dialogue System").GetComponent<DialogueManager>();
        IM = GameObject.Find("Canvas").GetComponent<InventoryMenu>();
        AT = GameObject.Find("Canvas").GetComponent<ActionTracker>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        this.transform.position = GM.getLocation();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        //if (!canMove) return; // Don't let the player move

        //Records the input of the player using the Unity Default Horizontal Axis.  Allows for both controller and keyboard input.
        float horizontalInput = Input.GetAxis("Horizontal");

        //Moves the player using transform translate which allows for movement in both directions.  Right is the positive direction because of Vector3.right.
        transform.Translate(Vector3.right * playerspeed * horizontalInput * Time.deltaTime);

        //Records the input of the player using the Unity Default Vertical Axis.  Allows for both controller and keyboard input.
        float verticalInput = Input.GetAxis("Vertical");

        //Moves the player using transform translate which allows for movement in both directions.  Up is the positive direction because of Vector3.up
        transform.Translate(Vector3.up * playerspeed * verticalInput * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) && inTriggerPickUp)
        {
            //Place the UI activation and inventory addition for Items here
            //Debug.Log("UI Pickup and Inventory Addition");
            if(currPickUp != null)
            {
                currPickUp.GetComponent<PickUp>().addToInventory();
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //call a compare tag on the collison then set the appropriate bool to true.
        if (collision.tag == "Lilly") 
        {
            inTriggerNPC();
        }
        if (collision.tag == "Gertrude")
        {
            inTriggerNPC();
            switch (gertrudeIndex)
            {
                case 0: // Talk to gertrude for the first time
                    DM.GertrudeFirstMeet();
                    gertrudeIndex++;
                    break;
                case 1: // Talk to gertrude again 

                    // Player has both mask & oxygen tank and gives it to Gertrude
                    if (IM.checkForItem(4) && IM.checkForItem(6)) 
                    {
                        AT.useAnAction();
                        DM.GiveGertrudeOxygen();
                        gertrudeIndex++;
                        gertrudeTrigger.SetActive(false);
                        
                    } else // Player doesnt have the correct item
                    {
                        DM.GertrudeWaitingOnItem();
                    }
                    break;
                case 2: // Talk to gertrude on roof

                    break;
                default:
                    Debug.Log("ERROR: Issue with gertrude dialogue");
                    break;
            }

            
            
        }
        if (collision.tag == "Harold")
        {
            inTriggerNPC();
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
            GM.rememberLocation(this.transform.position);
            sceneChanger sceneChanger = collision.gameObject.GetComponent<sceneChanger>();
            sceneChanger.changeScene();
        }

        if (collision.tag == "Enemy" || collision.tag == "Projectile") 
        {
            Debug.Log("Kill/Damage Player" + collision.name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //call a compare tag on the collison then set the appropriate bool to false.  
        if (collision.tag == "PickUp")
        {
            inTriggerPickUp = false;
            currPickUp = null;
        }
        if (collision.tag == "Gertrude" || collision.tag == "Lilly" || collision.tag == "Harold")
        {
            canMove = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Projectile" || collision.collider.tag == "PatrolEnemy") 
        {
            Debug.Log("Kill/Damage Player" + collision.collider.name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void inTriggerNPC()
    {
        canMove = false;
    }

}
