using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Managers
    //private UIManager UI;
    //private AudioManager AU;


    // Start is called before the first frame update
    void Start()
    {
        
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

}
