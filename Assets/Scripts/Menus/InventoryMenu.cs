using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    public GameObject inventoryMenuUI;// the inventory menu object
    public static bool inventoryOpened = false;

    public GameObject cursor, backButton;
    [SerializeField] public GameObject[] locations;// 7 locations for cursor, 0 is back button, 1 - 6 are slots 1 - 6
    public int currentPosition = 0;// int keeps track of location

    [SerializeField] public Sprite[] itemImages;
    public string[] itemDescriptions;

    private PauseMenu PM;
    public bool isGamePaused = false;

    void Start()
    {
        updatePosition();
        Resume();
        PM = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isGamePaused || Input.GetKeyDown(KeyCode.Tab) && !isGamePaused)//open and close inventory
        {
            if (inventoryOpened)
            {
                Resume();
            }
            else
            {
                OpenMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.D) && inventoryOpened && !isGamePaused)// move on menu via 7 state array
        {
            if (currentPosition == 6)
            {
                currentPosition = 0;
            }
            else
            {
                currentPosition++;
            }
            updatePosition();
        }
        else if (Input.GetKeyDown(KeyCode.A) && inventoryOpened && !isGamePaused)
        {
            if(currentPosition == 0)
            {
                currentPosition = 6;
            }
            else
            {
                currentPosition--;
            }
            updatePosition();
        }


        if (Input.GetKeyDown(KeyCode.Return) && inventoryOpened && currentPosition == 0 && !isGamePaused)// if enter is pressed on back button, close menu
        {
            Resume();
        }

        //this if statement will be deleted later when code to pick up items off ground is addeds
        if (Input.GetKeyDown(KeyCode.M))//temp code to add items M is wood, N is stone, B is syringe
        {
            getItem(0);
        }else if (Input.GetKeyDown(KeyCode.N))
        {
            getItem(1);
        }else if (Input.GetKeyDown(KeyCode.B))
        {
            getItem(2);
        }

            CheckIfGameIsPaused();
    }

    void Resume()
    {
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        inventoryOpened = false;
        currentPosition = 0;
        updatePosition();
    }

    void OpenMenu()
    {
        inventoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        inventoryOpened = true;
    }

    void updatePosition()// updates the cursor to the new position when moved
    {
        cursor.transform.position = locations[currentPosition].transform.position;
        if (currentPosition == 0)
        {
            cursor.SetActive(false);
            backButton.SetActive(true);
        }
        else
        {
            cursor.SetActive(true);
            backButton.SetActive(false);
        }
    }

    //for now, 0 is wood plank, 1 is a rock, 2 is a syringe
    void getItem(int itemGotten)
    {
        //do get item code here
    }

    void CheckIfGameIsPaused()//checks if game is paused from the other script
    {
        isGamePaused = PM.returnPauseStatus();
    }
}
