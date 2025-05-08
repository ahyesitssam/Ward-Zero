using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("----Game State----")]
    [Range(0, 4)]
    public int gameState = 0;
    public const int gsMenu = 0;
    public const int gsPlaying = 1;
    public const int gsPaused = 2;
    public const int gsShop = 3;
    public const int gsGameOver = 4;

    [Header("----Player Settings----")]
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private GameObject playerInstance;
    Vector3 placeToMove;
    public bool basementOpen = false;

    // Managers
    //private UIManager UI;
    //private AudioManager AU;


    // Start is called before the first frame update
    void Start()
    {
        basementOpen = false;
    }

    public void basement()
    {
        basementOpen = true;
    }

    public void spawnPlayer()
    {
        if (playerInstance != null)
        {
            Instantiate(playerInstance, spawnPoint, Quaternion.identity);
        }
    }

    public void updateGameState(int state)
    {
        gameState = state;
    }

    public void rememberLocation(Vector3 transitionPoint) 
    {
        if(SceneManager.GetActiveScene().name == "Floor-1")
            spawnPoint = transitionPoint;
    }

    public Vector3 getLocation() 
    {
        if (placeToMove != new Vector3(0, 0, 0))
            return placeToMove;
        return spawnPoint;
    }
}
