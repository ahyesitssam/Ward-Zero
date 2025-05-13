using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;// the pause menu object
    public static bool gamePaused = false;

    public GameObject cursor;
    [SerializeField] public GameObject[] locations;// 4 locations for cursor, 0 = resume, 1 = settings, 2 = main menu, 3 = exit game
    public int currentPosition = 0;// int keeps track of location

    private GameManager GM;

    private InventoryMenu IM;
    public GameObject controls;
    bool controlsOpen = false;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>(); //link to the GameManager script
        updatePosition();
        DontDestroyOnLoad(this.gameObject);
        Resume();
        IM = GameObject.Find("Canvas").GetComponent<InventoryMenu>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //open and close pause menu
        {
            if (gamePaused)
            {
                Resume();
            }else{
                GM.updateGameState(2);
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && currentPosition != 3 && gamePaused)// move on pause menu via 4 state array
        {
            currentPosition++;
            updatePosition();
        }else if(Input.GetKeyDown(KeyCode.W) && currentPosition != 0 && gamePaused)
        {
            currentPosition--;
            updatePosition();
        }

        if (controlsOpen)
        {
            currentPosition = 0;
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                controls.SetActive(false);
                controlsOpen = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && gamePaused)// if enter is pressed do the thing the cursor is on
        {
            if (currentPosition == 0)
            {
                Resume();
            }else if (currentPosition == 1)
            {
                controls.SetActive(true);
                controlsOpen = true;
            }else if (currentPosition == 2)
            {
                IM.OpenMenu();
                Resume();
            }
            else if (currentPosition == 3)
            {
                Application.Quit();
            }
        }
    }

    void updatePosition()// updates the cursor to the new position when moved
    {
        cursor.transform.position = locations[currentPosition].transform.position;
        if(currentPosition == 3)
        {
            cursor.transform.localScale = new Vector3(.15f, .15f, .15f);
        }
        else
        {
            cursor.transform.localScale = new Vector3(.1f, .1f, .1f);
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        currentPosition = 0;
        updatePosition();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public bool returnPauseStatus()
    {
        return gamePaused;
    }
}
