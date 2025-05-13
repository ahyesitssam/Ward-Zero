using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int currentState = 0; // 0 is no selection, 1 is new game, 2 is settings, 3 is quit
    [SerializeField] public GameObject newGame;
    [SerializeField] public GameObject settings;
    [SerializeField] public GameObject quitGame;
    [SerializeField] public Sprite[] imageArray;

    [SerializeField] public GameObject canvas;

    [SerializeField] public GameObject controlsA;

    public bool controlsOpenA = false;

    private DialogueManager DM;

    private void Start()
    {
        DM = GameObject.Find("Dialogue System").GetComponent<DialogueManager>();
        currentState = 1;
    }

    void Update()
    {
        UpdateImage();
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            DoButton(); // presses the button
        }
        
        if (Input.GetKeyDown(KeyCode.W)  || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (!firstInput())
            {
                currentState--;
            }
            checkIfInBounds();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (!firstInput())
            {
                currentState++;
            }
            checkIfInBounds();
        }

        if (controlsOpenA)
        {
            currentState = 2;
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                controlsA.SetActive(false);
                controlsOpenA = false;
            }
        }

    }

    private void checkIfInBounds()
    {
        if (currentState >= 4)
        {
            currentState = 1;
        }
        if (currentState <= 0)
        {
            currentState = 3;
        }
    }

    private bool firstInput()
    {
        if (currentState == 0)//first button push
        {
            currentState = 1;
            return true;
        }
        return false;
    }


    private void UpdateImage()
    {
        
        if (currentState == 1)
        {
            newGame.GetComponent<SpriteRenderer>().sprite = imageArray[1];
            settings.GetComponent<SpriteRenderer>().sprite = imageArray[2];
            quitGame.GetComponent<SpriteRenderer>().sprite = imageArray[4];
        }
        else if (currentState == 2)
        {
            newGame.GetComponent<SpriteRenderer>().sprite = imageArray[0];
            settings.GetComponent<SpriteRenderer>().sprite = imageArray[3];
            quitGame.GetComponent<SpriteRenderer>().sprite = imageArray[4];
        }
        else if (currentState == 3)
        {
            newGame.GetComponent<SpriteRenderer>().sprite = imageArray[0];
            settings.GetComponent<SpriteRenderer>().sprite = imageArray[2];
            quitGame.GetComponent<SpriteRenderer>().sprite = imageArray[5];
        }
    }

    private void DoButton()
    {
        if (currentState == 1)
        {
            canvas.SetActive(true);
            StartGame();
        }
        else if (currentState == 2)
        {
            //Debug.Log("!");
            controlsA.SetActive(true);
            //Debug.Log(".");
            controlsOpenA = true;
            //Debug.Log("?");
        }
        else if (currentState == 3)
        {
            EndGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Floor-1");
        DM.playerTalkStartGame();
    }

    private void EndGame()
    {
        Application.Quit();
        quitGame.SetActive(false);
    }
}
