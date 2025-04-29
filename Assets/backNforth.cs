using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class backNforth : MonoBehaviour
{
    [SerializeField] float min = 2f;
    [SerializeField] float max = 3f;
    RaycastHit2D hit;
    bool enemySummoned = false;
    [SerializeField] GameObject enemyToSpawn;

    [SerializeField] bool left = false;
    [SerializeField] bool right = false;
    [SerializeField] bool up = false;
    [SerializeField] bool down = false;

    // Use this for initialization
    void Start()
    {

        min = transform.position.y;
        max = transform.position.y + 3;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!hit && right)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min), transform.right, 5.0f); //Change the Vector2.left to whatever direction we want default forward to be.
            //Debug.Log("checking");
        }
        else if (!hit && left)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min), -transform.right, 5.0f);
        }
        else if (!hit && up)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min), transform.up, 5.0f);
        }
        else if (!hit && down)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min), -transform.up, 5.0f);
        }



        if (hit && hit.collider.tag == "Player" && hit.collider.tag != null && !enemySummoned)
        {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            Enemy enemyScript = enemyToSpawn.GetComponent<Enemy>();
            //enemyScript.spawned = true;
            enemySummoned = true;
        }
        else 
        {
            if (right)
            {
                hit = Physics2D.Raycast(new Vector2(this.transform.position.x + 1, Mathf.PingPong(Time.time * 2, max - min) + min), transform.right, 5.0f); //Change the Vector2.left to whatever direction we want default forward to be.
                //Debug.Log("checking");
            }
            else if (left)
            {
                hit = Physics2D.Raycast(new Vector2(this.transform.position.x - 1, Mathf.PingPong(Time.time * 2, max - min) + min), -transform.right, 5.0f);
            }
            else if (up)
            {
                hit = Physics2D.Raycast(new Vector2(this.transform.position.x, (Mathf.PingPong(Time.time * 2, max - min) + min) + 1), transform.up, 5.0f);
            }
            else if (down)
            {
                hit = Physics2D.Raycast(new Vector2(this.transform.position.x, (Mathf.PingPong(Time.time * 2, max - min) + min) - 1), -transform.up, 5.0f);
            }
                transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);
        }

    }
}
