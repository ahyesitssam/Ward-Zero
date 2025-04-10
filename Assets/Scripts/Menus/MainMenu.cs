using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int currentState = 0; // 0 is no selection, 1 is new game, 2 is quit
    [SerializeField] public GameObject mainImage;
    [SerializeField] public Sprite[] imageArray;

    // Update is called once per frame WHAT!?!?!?! IT IS!?!?!?!?! NO WAY!!!!!!!!!
    void Update()
    {
        UpdateImage();
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            DoButton(); // presses the button
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if (currentState == 0)//first button push
            {
                currentState = 1;
            }
            else
            {
                swapState();
            }
        }
    }

    private void swapState()//dumb code :(
    {
        if(currentState == 1)
        {
            currentState = 2;
        }else if(currentState == 2)
        {
            currentState = 1;
        }

    }

    private void UpdateImage()
    {
        mainImage.GetComponent<SpriteRenderer>().sprite = imageArray[currentState]; //updates the sprites
    }

    private void DoButton()
    {
        if(currentState == 1)
        {
            StartGame();
        }else if(currentState == 2)
        {
            EndGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1); // currently this is the hub room
    }

    private void EndGame()
    {
        Application.Quit();
        mainImage.SetActive(false);
    }
}
