using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    public GameObject inventoryMenuUI;// the inventory menu object
    public static bool inventoryOpened = false;

    public GameObject cursor, backButton;
    [SerializeField] public GameObject[] locations;// 7 locations for cursor, 0 is back button, 1 - 6 are slots 1 - 6
    public int currentPosition = 0;// int keeps track of location

    [SerializeField] public Sprite[] itemImages; //images of the items
    [SerializeField] public GameObject[] itemImageLocations; //images of the items
    private string[] itemDescriptions; //descriptions of items
    private int[] itemInventory = new int[6]; // the 6 slots in the inventory and what they have 
    private int itemSlot = 0; // the current slot to be filled

    private PauseMenu PM;
    private bool isGamePaused = false;

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
            addItem(1);
        }else if (Input.GetKeyDown(KeyCode.N))
        {
            addItem(2);
        }else if (Input.GetKeyDown(KeyCode.B))
        {
            addItem(3);
        }else if (Input.GetKeyDown(KeyCode.C))
        {
            clearInv();
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

    //for now,0 is empty, 1 is wood plank, 2 is a rock, 3 is a syringe
    void addItem(int itemAdded)
    {
        if (itemSlot != 6)//check if full
        {
            itemInventory[itemSlot] = itemAdded;// updates array of ints to have new item int
            itemImageLocations[itemSlot].SetActive(true); //sets it active
            itemSlot++; // increase inventory by 1
        }
        updateInventory();
    }

    void clearInv()//sets all to 0 
    {
        for (int i = 0; i < 6; i++)
        {
            itemInventory[i] = 0;
        }
        itemSlot = 0;
        updateInventory();
    }

    //updates the inventory text and images (doesnt do text for now)
    void updateInventory()
    {
        for (int i = 0; i < 6; i++)
        {
            itemImageLocations[i].GetComponent<Image>().sprite = itemImages[itemInventory[i]]; //updates the sprites
            if(itemInventory[i] == 0)
            {
                itemImageLocations[i].SetActive(false);
            }
        }
    }

    void CheckIfGameIsPaused()//checks if game is paused from the other script
    {
        isGamePaused = PM.returnPauseStatus();
    }
}
