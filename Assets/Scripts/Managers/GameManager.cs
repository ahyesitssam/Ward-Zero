using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("----Game State----")]
    [Range(0, 4)]
    [SerializeField] private int gameState = 0;
    public const int gsMenu = 0;
    public const int gsPlaying = 1;
    public const int gsPaused = 2;
    public const int gsShop = 3;
    public const int gsGameOver = 4;

    [Header("----Player Settings----")]
    [SerializeField] private Vector3 spawnPoint = new Vector3(0, 5, 0);
    //[SerializeField] Player P;
    //GameObject playerInstance;

    // Managers
    //private UIManager UI;
    //private AudioManager AU;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
