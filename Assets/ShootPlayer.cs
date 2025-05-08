using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;
    [SerializeField] GameObject projectile; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            StartCoroutine(fire());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fire() 
    {
        playerPos = player.transform.position;
        yield return new WaitForSeconds(1f);
        Instantiate(projectile, new Vector2(playerPos.x, playerPos.y + 5), Quaternion.identity);
        StartCoroutine(fire());
    }


}